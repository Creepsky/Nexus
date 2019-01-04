using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class TupleLiteral : Expression
    {
        public IList<IExpression> Values { get; set; }

        public override string ToString() => $"std::make_tuple({string.Join(", ", Values)})";

        public override SimpleType GetResultType(Context context) =>
            new SimpleType("tuple", new TemplateList(Values.Select(i => GetResultType(context)).ToList()), 0)
            {
                Line = Line,
                Column = Column 
            };

        public override void Check(Context context)
        {
            foreach (var i in Values)
            {
                i.Check(context);
            }
        }

        public override bool Print(PrintType type, Printer printer)
        {
            printer.Write("std::make_tuple(");
            foreach (var i in Values)
            {
                i.Print(type, printer);
                if (!Equals(i, Values.Last()))
                {
                    printer.Write(", ");
                }
            }
            printer.Write(")");
            return true;
        }

        public override object Clone()
        {
            return new TupleLiteral
            {
                Values = Values.Select(i => (IExpression) i.CloneDeep()).ToList()
            };
        }
    }
}