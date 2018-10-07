using System;
using System.Collections.Generic;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class TupleLiteral : Expression
    {
        public IList<IExpression> Values;

        public override string ToString() => $"std::make_tuple({string.Join(", ", Values)})";
        
        public override void Check(Context context)
        {
            throw new NotImplementedException();
        }
    }
}