using System;
using System.IO;
using Antlr4.Runtime;

namespace NexusParserAntlr
{
    class Program
    {
        static void Main(string[] args)
        {
            var textReader = new StreamReader(args[0]);
            var input = new AntlrInputStream(textReader);
            var lexer = new NexusLexer(input, Console.Out, Console.Error);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new NexusParser(tokenStream);
            //parser.RemoveErrorListeners();
            //parser.AddErrorListener(new ErrorListener());
            var parsed = parser.file();
        }
    }
}
