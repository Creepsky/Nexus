using System.Collections.Generic;
using System.Linq;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class IfStatement : Statement
    {
        public IExpression Condition { get; set; }
        public IList<IStatement> Then { get; set; }
        public IList<IStatement> Else { get; set; }

        public override void Check(Context context)
        {
            Condition.Check(context);

            var thenContext = context.StackNewContext(this);

            foreach (var i in Then)
            {
                i.Check(thenContext);
            }

            if (Else != null)
            {
                var elseContext = context.StackNewContext(this);

                foreach (var i in Else)
                {
                    i.Check(elseContext);
                }
            }
        }

        public override SimpleType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void)
            {
                FilePath = FilePath,
                Line = Line,
                Column = Column
            };

        public override bool Print(PrintType type, Printer printer)
        {
            printer.Write("if (");
            Condition.Print(type, printer);
            printer.WriteLine(")");
            printer.WriteLine("{");
            printer.Push();
            foreach (var i in Then)
            {
                i.Print(PrintType.FunctionSource, printer);
            }
            printer.Pop();
            printer.WriteLine("}");
            if (Else != null && Else.Any())
            {
                printer.WriteLine("else");
                printer.WriteLine("{");
                printer.Push();
                foreach (var i in Else)
                {
                    i.Print(PrintType.FunctionSource, printer);
                }
                printer.Pop();
                printer.WriteLine("}");
            }
            return true;
        }

        public override object Clone()
        {
            return new IfStatement
            {
                Condition = (IExpression) Condition.CloneDeep(),
                Then = Then.Select(i => (IStatement) i.CloneDeep()).ToList(),
                Else = Else?.Select(i => (IStatement) i.CloneDeep()).ToList()
            };
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            Condition.Template(context, concreteElement);
            
            foreach (var i in Then)
            {
                i.Template(context, concreteElement);
            }

            if (Else != null)
            {
                foreach (var i in Else)
                {
                    i.Template(context, concreteElement);
                }
            }
        }
    }
}