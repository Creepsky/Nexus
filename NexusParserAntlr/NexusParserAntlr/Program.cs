using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Antlr4.Runtime;
using NexusParserAntlr.Generation;

namespace NexusParserAntlr
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            var files = Directory.EnumerateFiles(args[0], "*.nx").Select(ParseFile).ToList();
            var printer = new Printer(Console.Out);
            var generator = new Generator(files);
            generator.Generate();
            generator.Check();
            return 0;
        }

        private static ir.File ParseFile(string path)
        {
            var file = File.OpenRead(path);
            var textReader = new StreamReader(file);
            var input = new AntlrInputStream(textReader);
            var lexer = new NexusLexer(input, Console.Out, Console.Error);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new NexusParser(tokenStream);
            var ast = parser.file();
            Debug.WriteLine(ast.ToStringTree(parser));
            var visitor = new NexusGrammarVisitor();
            return (ir.File) visitor.Visit(ast);
        }
    }
}
