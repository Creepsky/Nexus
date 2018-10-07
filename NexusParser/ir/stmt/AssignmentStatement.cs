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
            throw new System.NotImplementedException();
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