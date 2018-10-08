using Nexus.gen;

namespace Nexus.ir.expr
{
    public class BooleanLiteral : Expression
    {
        public bool Value;

        public override string ToString() => Value ? "true" : "false";

        public override IType GetResultType(Context context)
        {
            throw new System.NotImplementedException();
        }

        public override void Check(Context context)
        { }
    }
}