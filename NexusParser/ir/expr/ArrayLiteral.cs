using System.Collections.Generic;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class ArrayLiteral : Expression
    {
        public IList<IExpression> Values;

        public override string ToString() => '{' + string.Join(", ", Values) + '}';
        
        public override void Check(Context context)
        { }
    }
}