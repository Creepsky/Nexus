using System;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class RangeLiteral : Expression
    {
        public IExpression Start { get; set; }
        public IExpression End { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }

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
            printer.Write("TODO: RangeLiteral");
        }
    }
}