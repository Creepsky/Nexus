using System;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class RangeLiteral : Expression
    {
        public IExpression Start { get; set; }
        public IExpression End { get; set; }

        public override SimpleType GetResultType(Context context)
        {
            throw new NotImplementedException();
        }

        public override void Check(Context context)
        {
            throw new NotImplementedException();
        }

        public override bool Print(PrintType type, Printer printer)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            return new RangeLiteral
            {
                Start = (IExpression) Start.CloneDeep(),
                End = (IExpression) End.CloneDeep()
            };
        }
    }
}