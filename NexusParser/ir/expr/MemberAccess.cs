using System.Linq;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class MemberAccess : Expression
    {
        private IExpression Element { get; }
        private string Member { get; }

        public MemberAccess(IExpression element, string member)
        {
            Element = element;
            Member = member;
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context)
        {
            return GetVariable(context).GetResultType(context);
        }

        public override void Check(Context context)
        {
            GetVariable(context);
        }

        private Variable GetVariable(Context context)
        {
            var resultType = Element.GetResultType(context);

            var elementClass = context.Get<Class>(resultType.Name, this);

            var variable = elementClass.Variables.FirstOrDefault(i => i.Name == Member);

            if (variable == null)
            {
                throw new NotFoundException(this, "member variable", Member);
            }

            return variable;
        }

        public override void Print(PrintType type, Printer printer)
        {
            Element.Print(type, printer);
            printer.Write($".{Member}");
        }
    }
}