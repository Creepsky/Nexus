using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class ArrayLiteral : Expression
    {
        public IList<IExpression> Values { get; set; }
        private SimpleType _resultType;

        public override string ToString() => '{' + string.Join(", ", Values) + '}';

        public override SimpleType GetResultType(Context context)
        {
            return _resultType ?? (_resultType = new SimpleType("vector",
                       new TemplateList(new List<SimpleType> {Values.First().GetResultType(context)}), 0));
        }

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