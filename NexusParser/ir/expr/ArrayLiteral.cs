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

        //public override IGenerationElement Generate(Context context, GenerationPhase phase)
        //{
        //    return this;
        //}

        public override SimpleType GetResultType(Context context) =>
            Values.First().GetResultType(context);

        public override void Check(Context context)
        {
            // TODO: check if all values has the same value or are compatible
            foreach (var i in Values)
            {
                i.Check(context);
            }
        }

        public override bool Print(PrintType type, Printer printer)
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
            return true;
        }

        public override object Clone()
        {
            return new ArrayLiteral
            {
                Values = Values.Select(i => (IExpression) i.CloneDeep()).ToList()
            };
        }
    }
}