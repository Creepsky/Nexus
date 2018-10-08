using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class TupleLiteral : Expression
    {
        public IList<IExpression> Values;

        public override string ToString() => $"std::make_tuple({string.Join(", ", Values)})";

        public override IType GetResultType(Context context)
        {
            throw new NotImplementedException();
        }

        public override void Check(Context context)
        {
            throw new NotImplementedException();
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write("std::make_tuple(");
            foreach (var i in Values)
            {
                i.Print(type, printer);
                if (i != Values.Last())
                    printer.Write(", ");
            }
            printer.Write(")");
        }
    }
}