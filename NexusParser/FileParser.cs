using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Antlr4.Runtime;
using Nexus.gen;

namespace Nexus
{
    public static class FileParser
    {
        public static IList<ir.File> ParseDirectory(string path) =>
            Directory.EnumerateFiles(path, "*.nx").Select(ParseFile).ToList();

        public static ir.File ParseFile(string path)
        {
            var file = File.OpenRead(path);
            var textReader = new StreamReader(file);
            var input = new AntlrInputStream(textReader);
            var lexer = new NexusLexer(input, Console.Out, Console.Error);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new NexusParser(tokenStream);
            var ast = parser.file();
            var visitor = new NexusGrammarVisitor();
            return (ir.File) visitor.Visit(ast);
        }
        
        public static void SaveCompilationUnit(string outputFolder, CompilationUnit compilationUnit)
        {
            var headerPath = Path.Combine(outputFolder, compilationUnit.Name + ".hpp");
            var sourcePath = Path.Combine(outputFolder, compilationUnit.Name + ".cpp");
            File.WriteAllText(headerPath, compilationUnit.Header);
            File.WriteAllText(sourcePath, compilationUnit.Source);
        }

        public static void WriteFiles(string outputPath, IEnumerable<CompilationUnit> compilationUnits)
        {
            Directory.CreateDirectory(outputPath);
            foreach (var unit in compilationUnits)
            {
                SaveCompilationUnit(outputPath, unit);
            }
        }
    }
}