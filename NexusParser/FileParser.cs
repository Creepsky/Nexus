using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Nexus.gen;
using NLog;
using Zio;
using Zio.FileSystems;

namespace Nexus
{
    public static class FileParser
    {
        public static IList<ir.File> ParseProject(Configuration configuration)
        {
            return ParseDirectory("/", configuration).ToList();
        }

        public static IEnumerable<ir.File> ParseDirectory(string path, Configuration configuration)
        {
            var files = configuration.EnumerateSourceFiles(path)
                .Select(i => ParseFile(i, configuration));

            foreach (var i in configuration.EnumerateDirectories(path))
            {
                files = files.Concat(ParseDirectory(i, configuration));
            }

            return files;
        }

        public static ir.File ParseFile(string input)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            var inputStream = new AntlrInputStream(stream);
            return ParseFile(inputStream);
        }

        public static ir.File ParseFile(string path, Configuration configuration)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info($"Parsing '{path}'");
            var file = configuration.OpenFile(path);
            var textReader = new StreamReader(file);
            var input = new AntlrInputStream(textReader);
            var ir = ParseFile(input);
            ir.FilePath = path;
            return ir;
        }

        public static ir.File ParseFile(ICharStream input)
        {
            var lexer = new NexusLexer(input, Console.Out, Console.Error);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new NexusParser(tokenStream);
            var ast = parser.file();
            var visitor = new NexusGrammarVisitor();
            var ir = (ir.File)visitor.Visit(ast);
            return ir;
        }

        public static void WriteFiles(Configuration configuration, IEnumerable<CompilationUnit> compilationUnits)
        {
            var physicalFileSystem = new PhysicalFileSystem();
            var outputPath = Configuration.OsPathToUPath(configuration.OutputPath);

            if (!physicalFileSystem.DirectoryExists(outputPath))
            {
                physicalFileSystem.CreateDirectory(outputPath);
            }

            var fileSystem = new SubFileSystem(physicalFileSystem, outputPath);

            foreach (var unit in compilationUnits)
            {
                var path = (UPath) unit.Path;
                var dir = path.GetDirectory();

                if (!fileSystem.DirectoryExists(dir))
                {
                    fileSystem.CreateDirectory(dir);
                }

                if (!string.IsNullOrWhiteSpace(unit.Header))
                {
                    fileSystem.WriteAllText(dir / unit.Name + ".hpp", unit.Header);
                }

                if (!string.IsNullOrWhiteSpace(unit.Source))
                {
                    fileSystem.WriteAllText(dir / unit.Name + ".cpp", unit.Source);
                }
            }
        }
    }
}