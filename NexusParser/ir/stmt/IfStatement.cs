using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.stmt
{
    public class IfStatement : Statement
    {
        public ICondition Condition { get; set; }
        public IList<IStatement> Then { get; set; }
        public IList<IStatement> Else { get; set; }

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
            printer.Write("if (");
            Condition.Print(type, printer);
            printer.WriteLine(")");
            printer.WriteLine("{");
            printer.Push();
            foreach (var i in Then)
                i.Print(PrintType.FunctionSource, printer);
            printer.Pop();
            printer.WriteLine("}");
            if (Else.Any())
            {
                printer.WriteLine("else");
                printer.WriteLine("{");
                printer.Push();
                foreach (var i in Else)
                    i.Print(PrintType.FunctionSource, printer);
                printer.Pop();
                printer.WriteLine("}");
            }
        }
    }
}