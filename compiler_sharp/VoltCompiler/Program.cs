using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sprache;

namespace Volt
{

    public static class VoltParser
    {
        public static readonly Parser<IEnumerable<string>> Whitespaces =
            Parse.LineTerminator.Or(Parse.String(" ").Text()).Many();

        public static Parser<string> Identifier =>
            from start in Parse.Letter.Once().Text()
            from rest in Parse.LetterOrDigit.Or(Parse.Chars("_-")).Many().Text()
            select start + rest;
        
        public static readonly Parser<string> QuotedText =
            from start in Parse.Char('"').Once()
            from text in Parse.AnyChar.Except(Parse.Char('"')).Many().Text()
            from end in Parse.Char('"').Once()
            select text;

        public static readonly Parser<dynamic> Factor =
            QuotedText
                .Or(Parse.Decimal)
                .Or(Parse.Number);

        public static readonly Parser<dynamic> Assignment =
            from equalSign in Parse.Char('=')
            from factor in Factor.Shift()
            select factor;

        public static readonly Parser<IClassMember> Variable =
            from type in Identifier.Named("variable type")
            from array in Parse.String("[]").Shift().Many().Optional()
            from name in Identifier.Shift().Named("variable name")
            from setter in Parse.String("set").Shift().Text().Optional()
            from getter in Parse.String("get").Shift().Text().Optional()
            from assignment in Assignment.Shift().Optional().Named("variable initialization")
            select new Variable
            {
                Type = type,
                Array = array.GetOrElse(new List<List<char>>()).Count(),
                Name = name,
                Setter = setter.GetOrDefault() != null,
                Getter = getter.GetOrDefault() != null,
                Initialization = assignment
            };

        //public static readonly Parser<dynamic> FunctionBody =
        //    Variable.

        public static readonly Parser<IClassMember> Function =
            from returnType in Identifier.Named("function return value")
            from name in Identifier.Shift().Named("function name")
            from parametersBegin in Parse.Char('(').Shift().Named("function parameters begin")
            from parameters in Variable.Shift().DelimitedBy(Parse.Char(',')).Optional().Named("function parameters")
            from parametersEnd in Parse.Char(')').Shift().Named("function parameters end")
            from bodyBegin in Parse.Char('{').Shift().Named("function body begin")
            //from body in FunctionBody.Named("function body")
            from bodyEnd in Parse.Char('}').Shift().Named("function body end")
            select new Function
            {
                Name = name, ReturnType = returnType,
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
            
            parsedContent.ToHeader(printer);
            parsedContent.ToSource(printer);

            return 0;
        }
    }
}
