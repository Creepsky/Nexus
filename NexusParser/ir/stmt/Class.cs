using System.Collections.Generic;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class Class : Statement
    {
        public ITemplateList TemplateList { get; }
        public IList<Variable> Variables { get; }
        public IList<CppBlockStatement> CppBlocks { get; }
        public IList<Include> Includes { get; }

        public SimpleType Type { get; }
        private Context _context;

        public Class(string name, ITemplateList templateList, IList<Variable> variables, IList<CppBlockStatement> cppBlocks, IList<Include> includes)
        {
            Name = name;
            Variables = variables;
            CppBlocks = cppBlocks;
            Type = new SimpleType(Name, 0, Line, Column);
            TemplateList = templateList;
            Includes = includes;
        }

        public override string ToString()
        {
            return $"class {Name}";
        }

        public override IGenerationElement Generate(Context upperContext, GenerationPhase phase)
        {
            if (phase == GenerationPhase.ForwardDeclaration)
            {
                upperContext.AddGlobal(Name, this);
                _context = upperContext.StackNewContext(this);
            }

            foreach (var i in Includes)
            {
                i.Generate(_context, phase);
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
                TemplateList?.Print(type, printer);
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