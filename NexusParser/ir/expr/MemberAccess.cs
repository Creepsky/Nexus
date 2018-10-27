using System.Linq;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class MemberAccess : Expression
    {
        private string Element { get; }
        private string Member { get; }

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
            return GetVariable(context).GetResultType(context);
        }

        public override void Check(Context context)
        {
            GetVariable(context);
        }

        private Variable GetVariable(Context context)
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

            var variable = elementClass.Variables.FirstOrDefault(i => i.Name == Member);

            if (variable == null)
            {
                throw new NotFoundException(this, "member variable", Member);
            }

            return variable;
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write($"{Element}.{Member}");
        }
    }
}