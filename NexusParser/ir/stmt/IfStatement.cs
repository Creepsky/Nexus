using System.Collections.Generic;
using Nexus.gen;

namespace Nexus.ir.stmt
{
    public class IfStatement : Statement
    {
        public ICondition Condition;
        public IList<IStatement> Then;
        public IList<IStatement> Else;

        public override void Check(Context context)
        {
            Condition.Check(context);

            var thenContext = context.StackNewContext(this);
            var elseContext = context.StackNewContext(this);

            foreach (var i in Then)
                i.Check(thenContext);

            foreach (var i in Else)
                i.Check(elseContext);
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override void Print(PrintType type, Printer printer)
        {
            throw new System.NotImplementedException();
        }
    }
}