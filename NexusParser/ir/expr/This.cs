using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class This : Expression
    {
        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context)
        {
            return context.UpperContext.GetElementAs<Class>(this).Type;
        }

        public override void Check(Context context)
        {
            // this can only exist inside a function..
            // TODO: what about extension functions?
            context.GetElementAs<Function>(this);
            // ..that is defined inside a class
            context.UpperContext.GetElementAs<Class>(this);
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write("this");
        }
    }
}