using System.Linq;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class FunctionCallStatement : Statement
    {
        public FunctionCall FunctionCall { get; set; }

        public override void Check(Context context)
        {
            FunctionCall.Check(context);
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void, 0, Line, Column);

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write($"{FunctionCall.Name}(");
            foreach (var i in FunctionCall.Parameter)
            {
                i.Print(type, printer);

                if (!i.Equals(FunctionCall.Parameter.Last()))
                {
                    printer.Write(", ");
                }
            }
            printer.WriteLine(");");
        }
    }
}