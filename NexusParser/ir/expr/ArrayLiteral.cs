using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class ArrayLiteral : Expression
    {
        public IList<IExpression> Values { get; set; }

        public override string ToString() => '{' + string.Join(", ", Values) + '}';

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context) =>
            Values.First().GetResultType(context);

        public override void Check(Context context)
        {
            // TODO: check if all values has the same value or are compatible
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write("{");
            foreach (var i in Values)
            {
                i.Print(type, printer);

                if (!i.Equals(Values.Last()))
                {
                    printer.Write(", ");
                }
            }
            printer.Write("}");
        }
    }
}