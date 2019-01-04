using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class New : Expression
    {
        public SimpleType Type { get; set; }
        public IList<IExpression> Parameter { get; set; }

        public override SimpleType GetResultType(Context context)
        {
            return Type.GetResultType(context);
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            Type.Template(context, concreteElement);
            foreach (var i in Parameter)
            {
                i.Template(context, concreteElement);
            }
        }

        public override void Check(Context context)
        {
            Type.Check(context);
            foreach (var i in Parameter)
            {
                i.Check(context);
            }
        }

        public override bool Print(PrintType type, Printer printer)
        {
            Type.Print(type, printer);

            printer.Write("(");

            foreach (var i in Parameter)
            {
                if (i.Print(type, printer) && !Equals(i, Parameter.Last()))
                {
                    printer.Write(", ");
                }
            }

            printer.Write(")");

            return true;
        }

        public override object Clone()
        {
            return new New
            {
                Type = (SimpleType)Type.Clone(),
                Parameter = Parameter?.Select(i => (IExpression)i.Clone()).ToList()
            };
        }

        public override string ToString()
        {
            return $"new {Type}({string.Join(", ", Parameter)})";
        }
    }
}