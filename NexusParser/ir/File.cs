using System;
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
                
                foreach (var i in GetElements())
                {
                    i.FilePath = FilePath;
                }
            }
        }

        public IList<Include> Includes { get; } = new List<Include>();
        public IList<Class> Classes { get; set; }
        public IList<Function> Functions { get; set; }
        public IList<Function> GeneratedFunctions { get; set; } = new List<Function>();
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

            if (_context.Get(include.Name) == null)
            {
                _context.AddGlobal(include.Name, include);
            }
        }

        public SimpleType GetResultType(Context context)
        {
            throw new UnexpectedCallException(this, "File", "GetResultType");
        }

        public IGenerationElement CloneDeep()
        {
            var i = (File) Clone();
            i.Column = Column;
            i.Line = Line;
            i.FilePath = FilePath;
            return i;
        }

        public void ForwardDeclare(Context upperContext)
        {
            _context = upperContext.StackNewContext(this);

            foreach (var i in GetElements())
            {
                i.ForwardDeclare(_context);
            }
        }

        public void Declare()
        {
            foreach (var i in GetElements())
            {
                i.Declare();
            }
        }

        public void Define()
        {
            foreach (var i in GetElements())
            {
                i.Define();
            }
        }

        public void Remove()
        {
            foreach (var i in GetElements())
            {
                i.Remove();
            }
        }

        public void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            throw new NotSupportedException("A file can not be a template");
        }

        public bool Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                printer.WriteLine("#pragma once");
                printer.WriteLine();

                var printed = false;
            
                foreach (var i in GetElements())
                {
                    if (i.Print(type, printer))
                    {
                        printer.WriteLine();
                        printed = true;
                    }
                }

                return printed;
            }

            if (type == PrintType.Source)
            {
                printer.WriteLine($"#include \"{Name}.hpp\"");
                printer.WriteLine();

                if (Includes.Any())
                {
                    foreach (var i in Includes)
                    {
                        i.Print(type, printer);
                    }

                    printer.WriteLine();
                }

                var printed = false;
            
                foreach (var i in GetElements())
                {
                    if (i.Print(type, printer))
                    {
                        printer.WriteLine();
                        printed = true;
                    }
                }

                return printed;
            }

            return false;
        }

        public void Check(Context context)
        {
            foreach (var i in GetElements())
            {
                i.Check(context);
            }
        }

        private IEnumerable<IGenerationElement> GetElements()
        {
            return GenerationElementExtensions.GetAllElements(Classes, Functions, GeneratedFunctions, CppBlocks);
        }

        public object Clone()
        {
            throw new NotSupportedException("A file can not be copied");
        }
    }
}