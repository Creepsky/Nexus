using System.Linq;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class MemberAccess : Expression
    {
        public string Element { get; }
        public string Member { get; }

        public MemberAccess(string element, string member)
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
            throw new System.NotImplementedException();
        }

        public override void Check(Context context)
        {
            string elementName;

            if (Element == "this")
            {
                var extensionFunction = context.GetElementAs<ExtensionFunction>(this);
                elementName = extensionFunction.ExtensionBase.Name;
            }
            else
            {
                elementName = Element;
            }

            var element = context.Get(elementName, this);

            if (element == null)
            {
                throw new NotFoundException(this, "element", Element);
            }

            var elementType = element.GetResultType(context);
            var elementClass = context.Get<Class>(elementType.Name, this);

            if (elementClass.Variables.All(i => i.Name != Member))
            {
                throw new NotFoundException(this, "member variable", Member);
            }
        }

        public override void Print(PrintType type, Printer printer)
        {
            throw new System.NotImplementedException();
        }
    }
}