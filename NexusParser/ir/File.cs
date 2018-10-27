using System.Collections.Generic;
using System.Linq;
using Nexus.common;
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

        public string FilePath
        {
            get => _path;
            set
            {
                _path = value;
                Name = UPathExtensions.GetNameWithoutExtension(_path);
                
                foreach (var i in GetElements<IGenerationElement>())
                {
                    i.FilePath = FilePath;
                }
            }
        }

        public IList<Include> Includes { get; } = new List<Include>();
        public IList<Class> Classes { get; set; }
        public IList<ExtensionFunction> ExtensionFunctions { get; set; }
        public IList<CppBlockStatement> CppBlocks { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        private Context _context;

        public void Add(Include include)
        {
            if (Includes.All(i => i.Path != include.Path))
            {
                Includes.Add(include);
            }
        }

        public IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            if (phase == GenerationPhase.ForwardDeclaration)
            {
                _context = context.StackNewContext(this);
            }

            foreach (var i in GetElements<IGenerationElement>())
            {
                i.Generate(_context, phase);
            }

            return this;
        }

        public IType GetResultType(Context context)
        {
            throw new UnexpectedCallException(this, "File", "GetResultType");
        }
        
        public void Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                printer.WriteLine("#pragma once");
                printer.WriteLine();
            }
            
            foreach (var i in Includes.OfType<IPrintable>().Concat(GetElements<IPrintable>()))
            {
                i.Print(type, printer);
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
            return Classes.OfType<T>()
                .Concat(ExtensionFunctions.OfType<T>())
                .Concat(CppBlocks.OfType<T>());
        }
    }
}