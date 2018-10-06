using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Nexus.gen
{
    public struct CompilationUnit
    {
        public string Name;
        public string Header;
        public string Source;
    }

    public class Generator
    {
        private readonly Context _globalContext = new Context();

        public Generator(IList<ir.File> files)
        {
            foreach (var file in files)
                foreach (var c in file.Classes)
                    _globalContext.Add(c.Name, new Class(_globalContext, c,
                        files.SelectMany(i => i.ExtensionFunctions).Where(i => i.Class == c.Name)));
        }

        public void Generate()
        {
            foreach (var i in _globalContext.GetElements())
                i.Generate(_globalContext);
        }

        public void Check()
        {
            foreach (var i in _globalContext.GetElements())
                i.Check(_globalContext);
        }

        public IList<CompilationUnit> Compile()
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

                i.ToHeader(headerPrinter);
                i.ToHeader(sourcePrinter);

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