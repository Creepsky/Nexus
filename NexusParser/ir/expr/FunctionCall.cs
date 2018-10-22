using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class FunctionCall : Expression
    {
        public IList<IExpression> Parameter { get; set; }

        public override string ToString() => $"{Name}({string.Join(", ", Parameter)})";

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context) =>
            ((Function) context.Get(Name)).Type;

        public override void Check(Context context)
        {
            // TODO: check the signature of the function and the parameter
        }

        public override void Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                printer.Write(Name);
                printer.Write("(");
                foreach (var i in Parameter)
                {
                    i.Print(type, printer);
                    if (!i.Equals(Parameter.Last()))
                    {
                        printer.Write(", ");
                    }
                }
                printer.Write(")");
            }
        }
    }
}