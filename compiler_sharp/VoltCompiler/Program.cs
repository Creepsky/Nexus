using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using Sprache;

namespace Volt
{
    public abstract class ClassMember
    { }

    public class Variable : ClassMember
    {
        public string Type;
        public string Name;
        public IOption<dynamic> Initialization;

        public override string ToString()
        {
            return $"{Type} {Name}";
        }
    }

    public class Function : ClassMember
    {
        public string ReturnType;
        public string Name;
        public IList<Variable> Parameters;

        public override string ToString()
        {
            return $"{ReturnType} {Name}({string.Join(", ", Parameters)})";
        }
    }

    public class Class
    {
        public string Name;
        public List<ClassMember> Members;

        public override string ToString()
        {
            return Name;
        }
    }

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

        public static readonly Parser<ClassMember> Variable =
            from type in Identifier.Named("variable type")
            from name in Identifier.Shift().Named("variable name")
            from assignment in Assignment.Shift().Optional().Named("variable initialization")
            select new Variable {Type = type, Name = name, Initialization = assignment };

        //public static readonly Parser<dynamic> FunctionBody =
        //    Variable.

        public static readonly Parser<ClassMember> Function =
            from returnType in Identifier.Named("function return value")
            from name in Identifier.Shift().Named("function name")
            from parametersBegin in Parse.Char('(').Shift().Named("function parameters begin")
            from parameters in Variable.Shift().DelimitedBy(Parse.Char(',')).Optional().Named("function parameters")
            from parametersEnd in Parse.Char(')').Named("function parameters end")
            from bodyBegin in Parse.Char('{').Shift().Named("function body begin")
            //from body in FunctionBody.Named("function body")
            from bodyEnd in Parse.Char('}').Shift().Named("function body end")
            select new Function
            {
                Name = name, ReturnType = returnType,
                Parameters = parameters.GetOrElse(new List<ClassMember>()).Select(i => (Variable) i).ToList()
            };

        public static Parser<ClassMember> ClassMembers =>
            Function.Named("class function").Shift()
                .Or(Variable
                    .Then(member => Parse.Char(';')
                        .Named("class member delimiter ';'")
                        .Shift()
                        .Select(_ => member))
                    .Named("class variable"))
                .Shift();

        public static Parser<IEnumerable<ClassMember>> ClassDefinition =>
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

            return 0;
        }
    }
}
