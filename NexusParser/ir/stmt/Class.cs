using System.Collections.Generic;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class Class : Statement
    {
        public string Name { get; }
        public IList<Variable> Variables { get; }
        public IList<CppBlock> CppBlocks { get; }

        public readonly SimpleType Type;
        private Context _context;

        public Class(string name, IList<Variable> variables, IList<CppBlock> cppBlocks)
        {
            Name = name;
            Variables = variables;
            CppBlocks = cppBlocks;
            Type = new SimpleType(Name, 0, Line, Column);
        }

        public override string ToString()
        {
            return $"class {Name}";
        }

        public override IGenerationElement Generate(Context upperContext, GenerationPhase phase)
        {
            if (phase == GenerationPhase.ForwardDeclaration)
            {
                upperContext.Add(Name, this);
                _context = upperContext.StackNewContext(this);
            }

            foreach (var i in Variables)
            {
                i.Generate(_context, phase);
            }

            foreach (var i in CppBlocks)
            {
                i.Generate(_context, phase);
            }

            return this;
        }

        public override IType GetResultType(Context context) =>
            new SimpleType(PrimitiveType.Void.ToString(), 0, Line, Column);

        public override void Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                printer.WriteLine("#pragma once");
                printer.WriteLine();
                // TODO: add #include function
                //if (Public.Types.Any(i => i.IsArray()))
                //    printer.WriteLine("#include <vector>");
                //if (Public.Types.Any(i => i.Type == "string"))
                //    printer.WriteLine("#include <string>");
                printer.WriteLine();
                printer.WriteLine($"struct {Name}");
                printer.WriteLine("{");
                printer.Push();
                foreach (var i in Variables)
                {
                    i.Print(type, printer);
                }
                foreach (var i in CppBlocks)
                {
                    i.Print(type, printer);
                }
                printer.Pop();
                printer.WriteLine("};");
            }
            else if (type == PrintType.Source)
            {
                printer.WriteLine($"#include \"{Name}.hpp\"");
                printer.WriteLine();
            
                // constructor
                printer.WriteLine($"{Name}::{Name}()");
                printer.Push();
                printer.WriteLine("{ }");
                printer.Pop();
                printer.WriteLine();
            }
        }

        public override void Check(Context context)
        {
            foreach (var i in Variables)
            {
                i.Check(context);
            }
        }
    }
}