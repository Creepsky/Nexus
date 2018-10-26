using System;
using System.Collections.Generic;
using System.IO;
using Nexus.ir.stmt;
using NLog;

namespace Nexus.gen
{
    public struct CompilationUnit
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Header { get; set; }
        public string Source { get; set; }
    }

    public class Generator
    {
        private readonly Context _globalContext = new Context();
        private readonly IList<ir.File> _files;
        private readonly Logger _logger;

        public Generator(IList<ir.File> files)
        {
            _files = files;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Generate()
        {
            var phases = new[]
            {
                GenerationPhase.ForwardDeclaration,
                GenerationPhase.Declaration,
                GenerationPhase.Definition
            };

            foreach (var phase in phases)
            {
                _logger.Info($"Generating data, phase {phase.ToString()}");
                foreach (var file in _files)
                {
                    file.Generate(_globalContext, phase);
                }
            }
        }

        public void Check()
        {
            foreach (var i in _files)
            {
                i.Check(_globalContext);
            }
        }

        public IEnumerable<CompilationUnit> Compile()
        {
            var compilationUnits = new List<CompilationUnit>();
            var headerStringWriter = new StringWriter();
            var sourceStringWriter = new StringWriter();
            var headerPrinter = new Printer(headerStringWriter);
            var sourcePrinter = new Printer(sourceStringWriter);

            foreach (var i in _files)
            {
                headerStringWriter.GetStringBuilder().Clear();
                sourceStringWriter.GetStringBuilder().Clear();

                i.Print(PrintType.Header, headerPrinter);
                i.Print(PrintType.Source, sourcePrinter);

                compilationUnits.Add(new CompilationUnit{
                    Name = i.Name,
                    Path = i.FilePath,
                    Header = headerStringWriter.ToString(),
                    Source = sourceStringWriter.ToString()
                });
            }

            return compilationUnits;
        }
    }
}