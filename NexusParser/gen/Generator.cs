using System.Collections.Generic;
using System.Linq;

namespace Nexus.gen
{
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
    }
}