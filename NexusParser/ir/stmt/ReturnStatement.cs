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

            var function = context.GetElementAs<Function>(this);
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write("return ");
            Value.Print(type, printer);
            printer.WriteLine(";");
        }
    }
}