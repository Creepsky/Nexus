using System;
using System.Diagnostics;
using System.IO;
using Antlr4.Runtime;

namespace NexusParserAntlr
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var textReader = new StreamReader(args[0]);
            var input = new AntlrInputStream(textReader);
            var lexer = new NexusLexer(input, Console.Out, Console.Error);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new NexusParser(tokenStream);
            //parser.RemoveErrorListeners();
            //parser.AddErrorListener(new ErrorListener());
            var ast = parser.file();
            Debug.WriteLine(ast.ToStringTree(parser));
            var visitor = new NexusGrammarVisitor();
            var file = visitor.Visit(ast);
        }
    }
}
