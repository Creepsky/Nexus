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
                    foreach (var i in file.Elements)
                    {
                        i.Generate(_globalContext, phase);
                    }
                }
            }
        }

        public void Check()
        {
            foreach (var i in _globalContext.GetElements())
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

            foreach (var i in _globalContext.GetElements())
            {
                headerStringWriter.GetStringBuilder().Clear();
                sourceStringWriter.GetStringBuilder().Clear();

                i.Print(PrintType.Header, headerPrinter);
                i.Print(PrintType.Source, sourcePrinter);

                if (i.GetType() == typeof(Class) ||
                    i.GetType() == typeof(ExtensionFunction))
                {
                    compilationUnits.Add(new CompilationUnit{
                        Name = i.GetType() == typeof(Class) ? ((Class) i).Name : ((ExtensionFunction) i).Name,
                        Path = i.GetType() == typeof(Class) ? ((Class) i).Path : ((ExtensionFunction) i).Path,
                        Header = headerStringWriter.ToString(),
                        Source = sourceStringWriter.ToString()
                    });
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(i), $"Invalid generation element type: {i.GetType().Name}");
                }
            }

            return compilationUnits;
        }
    }
}