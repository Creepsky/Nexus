using System.Collections.Generic;
using Nexus.gen;

namespace Nexus.ir.stmt
{
    public class WhileStatement : Statement
    {
        public ICondition Condition { get; set; }
        public IList<IStatement> Body { get; set; }

        public override void Check(Context context)
        {
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write("while (");
            Condition.Print(type, printer);
            printer.WriteLine(")");
            printer.WriteLine("{");
            printer.Push();
            foreach (var i in Body)
                i.Print(PrintType.FunctionSource, printer);
            printer.Pop();
            printer.WriteLine("}");
        }
    }
}