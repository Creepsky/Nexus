using System.Collections.Generic;
using System.Linq;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class ForStatement : Statement
    {
        public IExpression Start { get; set; }
        public IExpression Stop { get; set; }
        public IExpression Step { get; set; }
        public IList<IStatement> Body { get; set; }

        private Context _context;

        public override void Check(Context context)
        {
            foreach (var i in GetElements())
            {
                i.Check(_context);
            }
        }

        public override SimpleType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void)
            {
                FilePath = FilePath,
                Line = Line,
                Column = Column
            };

        public override void ForwardDeclare(Context upperContext)
        {
            _context = upperContext.StackNewContext(this);

            foreach (var i in GetElements())
            {
                i.ForwardDeclare(_context);
            }
        }

        public override void Declare()
        {
            foreach (var i in GetElements())
            {
                i.Declare();
            }
        }

        public override void Define()
        {
            foreach (var i in GetElements())
            {
                i.Define();
            }
        }

        public override void Remove()
        {
            foreach (var i in GetElements())
            {
                i.Remove();
            }
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            _context = context;

            foreach (var i in GetElements())
            {
                i.Template(context, concreteElement);
            }
        }

        public override bool Print(PrintType type, Printer printer)
        {
            printer.WriteLine($"// {ToString()}");
            printer.WriteLine("{");
            printer.Push();
            Start.Print(PrintType.ForSource, printer);
            printer.Write("for (");
            printer.Write("; ");
            Stop.Print(type, printer);
            printer.Write("; ");
            Step.Print(type, printer);
            printer.WriteLine(")");
            printer.WriteLine("{");
            printer.Push();
            foreach (var i in Body)
            {
                i.Print(type, printer);
            }
            printer.Pop();
            printer.WriteLine("}");
            printer.Pop();
            printer.WriteLine("}");
            return true;
        }

        public override object Clone()
        {
            return new ForStatement
            {
                Body = Body.Select(i => (IStatement) i.CloneDeep()).ToList(),
                Start = (IExpression) Start.CloneDeep(),
                Stop = (IExpression) Stop.CloneDeep(),
                Step = (IExpression) Step.CloneDeep()
            };
        }

        private IEnumerable<IGenerationElement> GetElements()
        {
            return GenerationElementExtensions.GetAllElements(Body)
                .Prepend(Start)
                .Prepend(Stop)
                .Prepend(Step);
        }

        public override string ToString()
        {
            return $"for ({Start}; {Stop}; {Step})";
        }
    }
}