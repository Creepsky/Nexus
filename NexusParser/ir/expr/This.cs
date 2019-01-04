using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class This : Expression
    {
        public override SimpleType GetResultType(Context context)
        {
            return context.UpperContext.GetElementAs<Class>(this).Type;
        }

        public override void Check(Context context)
        {
            // this can only exist inside a function..
            context.GetElementAs<Function>(this);
        }

        public override bool Print(PrintType type, Printer printer)
        {
            printer.Write("this");
            return true;
        }

        public override object Clone()
        {
            return new This();
        }
    }
}