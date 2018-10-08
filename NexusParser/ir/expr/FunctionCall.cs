using System;
using System.Collections.Generic;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class FunctionCall : Expression
    {
        public string Name;
        public IList<IExpression> Parameter;

        public override string ToString() => $"{Name}({string.Join(", ", Parameter)})";

        public override IType GetResultType(Context context)
        {
            return ((Function) context.Get(Name)).Type;
        }

        public override void Check(Context context)
        {
            throw new NotImplementedException();
        }
    }
}