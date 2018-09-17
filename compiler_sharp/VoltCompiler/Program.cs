using System;
using System.Collections.Generic;
using System.IO;
using Sprache;

namespace VoltCompiler
{
    class Variable
    {
        public string Type;
        public string Name;

        public override string ToString()
        {
            return $"{Type} {Name}";
        }
    }

    class Class
    {
        public string Name;
        public List<Variable> Variables;

        public override string ToString()
        {
            return Name;
        }
    }

    static class VoltParser
    {
        public static Parser<Class> ClassParser =
            from classTag  in Parse.String("class")
            from 
            select new Class
            {
                Name = "",
                Variables = new List<Variable>()
            };
    }

    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Input file not set");
                return 1;
            }

            var content = File.ReadAllText(args[0]);
            var parsedContent = VoltParser.ClassParser.Parse(content);

            return 0;
        }
    }
}
