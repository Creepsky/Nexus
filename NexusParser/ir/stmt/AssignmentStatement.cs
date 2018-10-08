using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class AssignmentStatement : Statement
    {
        public IExpression Left;
        public IExpression Right;

        public override void Check(Context context)
        {
            
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override void Print(PrintType type, Printer printer)
        {
            Left.Print(type, printer);
            printer.Write(" = ");
            Right.Print(type, printer);
            printer.WriteLine(";");
        }
    }
}