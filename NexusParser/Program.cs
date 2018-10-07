using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Antlr4.Runtime;
using Nexus.gen;

namespace Nexus
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
            Directory.CreateDirectory(args[1]);
            foreach (var compilationUnit in generator.Compile())
                SaveCompilationUnit(args[1], compilationUnit);
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

        private static void SaveCompilationUnit(string outputFolder, CompilationUnit compilationUnit)
        {
            var headerPath = Path.Combine(outputFolder, compilationUnit.Name + ".hpp");
            var sourcePath = Path.Combine(outputFolder, compilationUnit.Name + ".cpp");
            File.WriteAllText(headerPath, compilationUnit.Header);
            File.WriteAllText(sourcePath, compilationUnit.Source);
        }
    }
}
