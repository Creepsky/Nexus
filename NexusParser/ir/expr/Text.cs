using Nexus.gen;

namespace Nexus.ir.expr
{
    public class Text : Expression
    {
        public string Value;

        public override string ToString() => '"' + Value + '"';
        public override void Check(Context context)
        { }
    }
}