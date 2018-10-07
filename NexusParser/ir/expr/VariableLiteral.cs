using System.Linq;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class VariableLiteral : Expression
    {
        public string Name;

        public override string ToString() => Name;
       
        public override void Check(Context context)
        {
            // does the variable exist in this or some higher context?
            var i = context.Get(Name);

            if (i == null)
                throw new VariableNotFoundException(this, Name);

            if (i.GetType() != typeof(Variable))
                throw new  TypeMismatchException(this, nameof(Variable), i.GetType().Name);
        }
    }
}