using Nexus.gen;

namespace Nexus.ir.expr
{
    public class ParenExpression : Expression
    {
        public IExpression Expression { get; set; }
        public override SimpleType GetResultType(Context context) => Expression.GetResultType(context);
        public override void ForwardDeclare(Context upperContext)
        {
            Expression.ForwardDeclare(upperContext);
        }

        public override void Declare()
        {
            Expression.Declare();
        }

        public override void Define()
        {
            Expression.Define();
        }

        public override void Remove()
        {
            Expression.Remove();
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            Expression.Template(context, concreteElement);
        }

        public override void Check(Context context)
        {
            Expression.Check(context);
        }

        public override bool Print(PrintType type, Printer printer)
        {
            printer.Write("(");
            Expression.Print(type, printer);
            printer.Write(")");
            return true;
        }

        public override object Clone()
        {
            return new ParenExpression
            {
                Expression = (IExpression) Expression.CloneDeep()
            };
        }

        public override string ToString()
        {
            return $"({Expression})";
        }
    }
}