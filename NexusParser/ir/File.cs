using System.Collections.Generic;
using System.Linq;
using Nexus.gen;
using Nexus.ir.expr;
using Nexus.ir.stmt;
using Zio;

namespace Nexus.ir
{
    public class File : IGenerationElement
    {
        private string _path;
        
        public string Name { get; set; }

        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                Name = UPathExtensions.GetNameWithoutExtension(_path);
                
                foreach (var i in GetElements<IGenerationElement>())
                {
                    i.Path = Path;
                }
            }
        }

        public IList<Class> Classes { get; set; }
        public IList<ExtensionFunction> ExtensionFunctions { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }

        public IGenerationElement Generate(Context context, GenerationPhase phase)
        {            
            foreach (var i in GetElements<IGenerationElement>())
            {
                i.Generate(context, phase);
            }

            return this;
        }

        public IType GetResultType(Context context)
        {
            throw new System.NotImplementedException();
        }
        
        public void Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                printer.WriteLine("#pragma once");
                printer.WriteLine();
            }
            
            foreach (var i in GetElements<IPrintable>())
            {
                i.Print(type, printer);
            }

            if (type == PrintType.Header)
            {
                printer.WriteLine();
            }
        }

        public void Check(Context context)
        {
            foreach (var i in GetElements<IGenerationElement>())
            {
                i.Check(context);
            }
        }

        private IEnumerable<T> GetElements<T>()
        {
            return Classes.OfType<T>().Concat(ExtensionFunctions.OfType<T>());
        }
    }
}