using System.Collections.Generic;
using Nexus.gen;

namespace Nexus.ir.stmt
{
    public class WhileStatement : Statement
    {
        public ICondition Condition;
        public IList<IStatement> Body;

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