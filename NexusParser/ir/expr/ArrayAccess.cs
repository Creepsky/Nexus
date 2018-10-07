using System;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class ArrayAccess : Expression
    {
        public string Name;
        public IExpression Index;

        public override string ToString() => $"{Name}[{Index}]";
        
        public override void Check(Context context)
        {
            var symbol = context.Get(Name);

            if (symbol.GetType() != typeof(IType))
                throw new Exception($"array access on a instance of type {symbol} at line {Line}, column {Column}");
        }
    }
}