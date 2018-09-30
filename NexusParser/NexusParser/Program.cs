using System;
using System.IO;
using System.Linq;

namespace NexusParser
{
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
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(content);
            var parser = new Parser(tokens);
            var parsedClass = parser.Parse();

            return 0;
        }
    }
}
