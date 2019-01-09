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
        private SimpleType _resultType;
        private Variable _variable;

        public MemberAccess(IExpression element, string member)
        {
            Element = element;
            Member = member;
        }

        public override SimpleType GetResultType(Context context) =>
            _resultType ?? (_resultType = GetVariable(context).GetResultType(context));

        public override void Check(Context context)
        {
            GetVariable(context);
        }

        private Variable GetVariable(Context context)
        {
            if (_variable == null)
            {
                var resultType = Element.GetResultType(context);

                var elementClass = context.Get<Class>(resultType.Name, this);

                var concreteClass = elementClass.GetVariant(resultType, context);

                _variable = concreteClass.Variables.FirstOrDefault(i => i.Name == Member);

                if (_variable == null)
                {
                    throw new NotFoundException(this, "member variable", Member);
                }
            }

            return _variable;
        }

        public override bool Print(PrintType type, Printer printer)
        {
            Element.Print(type, printer);
            printer.Write($".{Member}");
            return true;
        }

        public override object Clone()
        {
            return new MemberAccess((IExpression) Element.CloneDeep(), new string(Member));
        }

        public override string ToString()
        {
            return $"MemberAccess({Element}.{Member})";
        }
    }
}