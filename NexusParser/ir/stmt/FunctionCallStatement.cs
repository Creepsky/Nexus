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
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context) =>
            new SimpleType
            {
                Line = Line,
                Column = Column,
                Name = PrimitiveType.Void.ToString()
            };

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write($"{FunctionCall.Name}(");
            foreach (var i in FunctionCall.Parameter)
            {
                i.Print(type, printer);

                if (i != FunctionCall.Parameter.Last())
                    printer.Write(", ");
            }
            printer.WriteLine(");");
        }
    }
}