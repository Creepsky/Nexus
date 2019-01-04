using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class ExpressionStatement : Statement
    {
        public IExpression Expression { get; set; }

        public override void Check(Context context)
        {
            Expression?.Check(context);
        }

        public override SimpleType GetResultType(Context context)
        {
            return Expression.GetResultType(context) ?? new SimpleType(TypesExtension.Void);
        }

        public override void ForwardDeclare(Context upperContext)
        {
            Expression?.ForwardDeclare(upperContext);
        }

        public override void Declare()
        {
            Expression?.Declare();
        }

        public override void Define()
        {
            Expression?.Define();
        }

        public override void Remove()
        {
            Expression?.Remove();
        }

        public override bool Print(PrintType type, Printer printer)
        {
            Expression?.Print(type, printer);
            printer.WriteLine(";");
            return true;
        }

        public override object Clone()
        {
            return new ExpressionStatement
            {
                Expression = (IExpression) Expression.CloneDeep()
            };
        }
    }
}