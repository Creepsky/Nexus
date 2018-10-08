using System.Collections.Generic;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class ForStatement : Statement
    {
        public IStatement Start;
        public ICondition Stop;
        public IExpression Step;
        public IList<IStatement> Body;

        public override void Check(Context context)
        {
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write("for (");
            Start.Print(PrintType.ForSource, printer);
            printer.Write(" ");
            Stop.Print(type, printer);
            printer.Write("; ");
            Step.Print(type, printer);
            printer.WriteLine(")");
            printer.WriteLine("{");
            printer.Push();
            foreach (var i in Body)
                i.Print(type, printer);
            printer.Pop();
            printer.WriteLine("}");
        }
    }
}