using System;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class VariableLiteral : Expression
    {
        public string Name;

        public override string ToString() => Name;
       
        public override void Check(Context context)
        {
            throw new NotImplementedException();
        }
    }
}