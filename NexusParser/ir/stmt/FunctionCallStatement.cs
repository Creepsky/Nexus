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

        public override SimpleType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void)
            {
                Line = Line,
                Column = Column
            };

        public override bool Print(PrintType type, Printer printer)
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
            return true;
        }

        public override object Clone()
        {
            return new FunctionCallStatement
            {
                FunctionCall = (FunctionCall) FunctionCall.CloneDeep()
            };
        }
    }
}