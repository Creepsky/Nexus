using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class MapLiteral : Expression
    {
        public IDictionary<IExpression, IExpression> Values;

        public override string ToString()
        {
            return '{' + string.Join(", ", Values.Select(i => '{' + i.Key.ToString() + ", " + i.Value.ToString() + '}')) + '}';
        }

        public override IType GetResultType(Context context)
        {
            throw new NotImplementedException();
        }

        public override void Check(Context context)
        {
            throw new NotImplementedException();
        }
    }
}