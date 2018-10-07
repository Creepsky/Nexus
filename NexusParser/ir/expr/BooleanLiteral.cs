using Nexus.gen;

namespace Nexus.ir.expr
{
    public class BooleanLiteral : Expression
    {
        public bool Value;

        public override string ToString() => Value ? "true" : "false";

        public override void Check(Context context)
        { }
    }
}