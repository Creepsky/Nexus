using System.Linq;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class VariableLiteral : Expression
    {
        private SimpleType _returnType;

        public override string ToString() => $"VariableLiteral({Name})";

        public override SimpleType GetResultType(Context context) =>
            _returnType ?? (_returnType = context.Get(Name, this).GetResultType(context));

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            if (concreteElement != null)
            {
                throw new TemplateGenerationException(this, $"Can not template {this} from {concreteElement}");
            }

            if (context.TemplateVariables.ContainsKey(Name))
            {
                Name = context.TemplateVariables[Name].GetResultType(context).ToString();
            }
        }

        public override void Check(Context context)
        {
            if (Name == "this")
            {
                if (!(context.Element is Function))
                {
                    throw new UnexpectedScopeException(this, context.Element.GetType().Name, new []
                    {
                        typeof(Function).Name
                    });
                }
            }
            else
            {
                // does the variable exist in this or some higher context?
                context.Get<Variable>(Name, this);
            }
        }

        public override bool Print(PrintType type, Printer printer)
        {
            printer.Write(Name == "this" ? "__this" : Name);
            return true;
        }

        public override object Clone()
        {
            return new VariableLiteral();
        }
    }
}