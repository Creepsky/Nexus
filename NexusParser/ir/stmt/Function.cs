using System.Collections.Generic;
using System.Linq;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class Function : Statement
    {
        public IType Type { get; set; }
        public ITemplateList TemplateList { get; set; }
        public IList<Variable> Parameter { get; set; }
        public IList<IStatement> Statements { get; set; }
        public bool Const { get; set; }
        private Context _context;

        public override string ToString()
        {
            return $"{Type} {Name}({string.Join(',', Parameter.Select(i => i.Type))})";
        }

        public override void Check(Context upperContext)
        {
            Type.Check(_context);

            foreach (var i in Parameter)
            {
                i.Check(_context);
            }

            foreach (var i in Statements)
            {
                i.Check(_context);
            }
        }

        public override IGenerationElement Generate(Context upperContext, GenerationPhase phase)
        {
            if (phase == GenerationPhase.ForwardDeclaration)
            {
                upperContext.Add(Name, this);
                _context = upperContext.StackNewContext(this);
            }

            Type.Generate(_context, phase);

            foreach (var i in GetElements())
            {
                i.Generate(_context, phase);
            }

            return this;
        }

        public override IType GetResultType(Context context) => Type;

        public override void Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Source)
            {
                if (_context.UpperContext.Element.GetType() == typeof(ExtensionFunction))
                {
                    printer.Write($"auto {Name} = [](");
                    foreach (var i in Parameter)
                    {
                        i.Print(PrintType.Parameter, printer);
                        if (!i.Equals(Parameter.Last()))
                        {
                            printer.Write(", ");
                        }
                    }
                    printer.WriteLine(")");
                    printer.WriteLine("{");
                    printer.Push();
                    foreach (var i in Statements)
                    {
                        i.Print(PrintType.FunctionSource, printer);
                    }
                    printer.Pop();
                    printer.WriteLine("};");
                }
            }
        }

        private IEnumerable<IGenerationElement> GetElements()
        {
            return Parameter.OfType<IGenerationElement>()
                .Concat(Statements);
        }
    }
}