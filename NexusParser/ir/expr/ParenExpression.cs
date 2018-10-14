using Nexus.gen;

namespace Nexus.ir.expr
{
    public class ParenExpression : Expression
    {
        public IExpression Expression { get; set; }

        public override IType GetResultType(Context context)
        {
            return Expression.GetResultType(context);
        }

        public override void Check(Context context)
        {
            Expression.Check(context);
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write("(");
            Expression.Print(type, printer);
            printer.Write(")");
        }
    }
}