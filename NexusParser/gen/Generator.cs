using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nexus.ir.stmt;

namespace Nexus.gen
{
    public struct CompilationUnit
    {
        public string Name { get; set; }
        public string Header { get; set; }
        public string Source { get; set; }
    }

    public class Generator
    {
        private readonly Context _globalContext = new Context();
        private readonly IList<ir.File> _files;

        public Generator(IList<ir.File> files)
        {
            _files = files;
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
                foreach (var file in _files)
                {
                    foreach (var c in file.Classes)
                    {
                        c.Generate(_globalContext, phase);
                    }
                }
            }
        }

        public void Check()
        {
            foreach (var i in _globalContext.GetElements())
                i.Check(_globalContext);
        }

        public IEnumerable<CompilationUnit> Compile()
        {
            var compilationUnits = new List<CompilationUnit>();
            var headerStringWriter = new StringWriter();
            var sourceStringWriter = new StringWriter();
            var headerPrinter = new Printer(headerStringWriter);
            var sourcePrinter = new Printer(sourceStringWriter);

            foreach (var i in _globalContext.GetElements()
                .Where(i => i.GetType() == typeof(Class))
                .Select(i => (Class)i))
            {
                headerStringWriter.GetStringBuilder().Clear();
                sourceStringWriter.GetStringBuilder().Clear();

                i.Print(PrintType.Header, headerPrinter);
                i.Print(PrintType.Source, sourcePrinter);

                compilationUnits.Add(new CompilationUnit{
                    Name = i.Name,
                    Header = headerStringWriter.ToString(),
                    Source = sourceStringWriter.ToString()
                });
            }

            return compilationUnits;
        }
    }
}