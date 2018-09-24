using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sprache;

namespace Nexus
{
    public static class VoltParser
    {
        public static readonly Parser<IEnumerable<string>> Whitespaces =
            Parse.LineTerminator.Or(Parse.String(" ").Text()).Many();

        public static Parser<string> Identifier =>
            from start in Parse.Letter.Once().Text()
            from rest in Parse.LetterOrDigit.Or(Parse.Chars("_-")).Many().Text()
            select start + rest;
        
        public static readonly Parser<IExpression> QuotedText =
            from start in Parse.Char('"').Once()
            from text in Parse.AnyChar.Except(Parse.Char('"')).Many().Text()
            from end in Parse.Char('"').Once()
            select new Text {Value = text};

        public static readonly Parser<string> SignedInteger =
            from sign in Parse.Chars("-+").Named("sign").Optional()
            from integer in Parse.Number.Named("integer")
            select $"{sign.GetOrElse('+')}{integer}";

        public static readonly Parser<string> UnsignedInteger =
            Parse.Number.Named("unsigned integer");

        public static readonly Parser<string> Real =
            from integerPart in SignedInteger
            from dot in Parse.Char('.')
            from decimalPart in Parse.Number.Named("decimal part").Optional()
            select $"{integerPart}.{decimalPart.GetOrDefault()}";

        public static readonly Parser<F32> F32 =
            from real in Real
            from suffix in Parse.String("f")
            select new F32{ Value = float.Parse(real) };

        public static readonly Parser<F64> F64 =
            from real in Real
            from suffix in Parse.String("d")
            select new F64{ Value = double.Parse(real) };

        public static readonly Parser<IExpression> Number =
            from sign in Parse.Chars("-+").Named("sign").Optional()
            from integerPart in Parse.Number.Named("integer part")
            from dot in Parse.Char('.').Named("dot").Optional()
            from decimalPart in Parse.Number.Named("decimal part").Optional()
            from suffix in Parse.Chars("iuf13468").Named("suffix").Many().Text().Optional()
            select NumberLiteral.Parse(
                sign.GetOrElse('+'),
                integerPart,
                decimalPart.GetOrDefault(),
                suffix.GetOrDefault());

        public static Parser<IExpression> Factor => QuotedText.Or(Number);

        public static readonly Parser<TypeDefinition> Type =
            from type in Identifier.Named("variable type")
            from array in Parse.String("[]").Shift().Many().Optional()
            select new TypeDefinition(type, array.GetOrElse(new List<List<char>>()).Count());

        public static readonly Parser<IExpression> Assignment =
            from equalSign in Parse.Char('=')
            from expression in Expression.Shift()
            select expression;

        public static readonly Parser<IClassMember> Variable =
            from type in Type
            from name in Identifier.Shift().Named("variable name")
            from setter in Parse.String("set").Shift().Text().Optional()
            from getter in Parse.String("get").Shift().Text().Optional()
            from assignment in Assignment.Shift().Optional().Named("variable initialization")
            select new Variable
            {
                Type = type,
                Name = name,
                Setter = setter.GetOrDefault() != null,
                Getter = getter.GetOrDefault() != null,
                Initialization = assignment
            };

        //public static readonly Parser<dynamic> FunctionBody =
        //    Variable.

        public static readonly Parser<IClassMember> Function =
            from returnType in Type.Named("function return value")
            from name in Identifier.Shift().Named("function name")
            from parametersBegin in Parse.Char('(').Shift().Named("function parameters begin")
            from parameters in Variable.Shift().DelimitedBy(Parse.Char(',')).Optional().Named("function parameters")
            from parametersEnd in Parse.Char(')').Shift().Named("function parameters end")
            from bodyBegin in Parse.Char('{').Shift().Named("function body begin")
            //from body in FunctionBody.Named("function body")
            from bodyEnd in Parse.Char('}').Shift().Named("function body end")
            select new Function
            {
                Name = name,
                ReturnType = returnType,
                Parameters = parameters.GetOrElse(new List<IClassMember>()).Select(i => (Variable) i).ToList()
            };

        public static Parser<IClassMember> ClassMembers =>
            Function.Named("class function").Shift()
                .Or(Variable
                    .Then(member => Parse.Char(';')
                        .Named("class member delimiter ';'")
                        .Shift()
                        .Select(_ => member))
                    .Named("class variable"))
                .Shift();

        public static Parser<IEnumerable<IClassMember>> ClassDefinition =>
            ClassMembers.Named("class member").Many();

        public static Parser<Class> Class =>
            from begin in Parse.String("class").Named("'class' keyword")
            from name in Identifier.Named("class name").Shift()
            from start in Parse.Char('{').Named("class definition start '{'").Shift()
            from definition in ClassDefinition.Named("class member")
            from end in Parse.Char('}').Named("class definition end '}'").Shift()
            select new Class {Name = name, Members = definition.ToList()};

        public static Parser<IExpression> VariableLiteral =>
            from identifier in Identifier
            select new VariableLiteral
            {
                Name = identifier
            };

        public static Parser<IExpression> ArrayLiteral =>
            from identifier in Identifier
            from lparen in Parse.Char('[').Shift().Named("left parentheses")
            from index in Expression.Shift().Named("index")
            from rparen in Parse.Char(']').Shift()
            select new ArrayLiteral
            {
                Name = identifier,
                Index = index
            };

        public static Parser<IExpression> Expression =>
            Factor
                .Or(VariableLiteral)
                .Or(ArrayLiteral);

        public static Parser<IExpression> Comparison =>
            from lhs in Expression.Named("comparison left side")
            from op in Parse.Chars("=<>!").Many().Text().Shift()
            from rhs in Expression.Named("comparison right side").Shift()
            select new Comparison(lhs, op, rhs);

        public static Parser<T> Shift<T>(this Parser<T> parser) =>
            Whitespaces.Then(_ => parser).Or(parser);
    }

    internal static class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Input file not set");
                return 1;
            }

            var content = File.ReadAllText(args[0]);
            var parsedContent = VoltParser.Class.Parse(content);
            
            var printer = new Printer(Console.Out);
            
            parsedContent.Compile(printer, printer);

            return 0;
        }
    }
}
