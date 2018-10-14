using System.Collections.Generic;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class ForStatement : Statement
    {
        public IStatement Start { get; set; }
        public ICondition Stop { get; set; }
        public IExpression Step { get; set; }
        public IList<IStatement> Body { get; set; }

        public override void Check(Context context)
        {
            // TODO
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void, Line, Column);

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