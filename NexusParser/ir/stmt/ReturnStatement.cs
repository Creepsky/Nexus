using Nexus.common;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class ReturnStatement : Statement
    {
        public IExpression Value;

        public override void Check(Context context)
        {
            if (context.Element == null)
                throw new PositionedException(this, "return statement without parent scope");

            if (context.Element.GetType() == typeof(Function))
            {

            }
        }

        public override IGenerationElement Generate(Context context)
        {
            throw new System.NotImplementedException();
        }

        public override void Print(PrintType type, Printer printer)
        {
            throw new System.NotImplementedException();
        }
    }
}