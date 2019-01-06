using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class New : Expression
    {
        public SimpleType Type { get; set; }
        public IDictionary<string, IExpression> Parameter { get; private set; } = new Dictionary<string, IExpression>();
        public IExpression SingleParameter { get; set; }
        private readonly IList<IExpression> _sortedParameter = new List<IExpression>();

        public override SimpleType GetResultType(Context context)
        {
            return Type.GetResultType(context);
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            Type.Template(context, concreteElement);

            SingleParameter?.Template(context, concreteElement);

            foreach (var i in Parameter)
            {
                i.Value.Template(context, concreteElement);
            }
        }

        public override void Check(Context context)
        {
            Type.Check(context);

            SingleParameter?.Check(context);

            var type = context.Get<Class>(Type.GetResultType(context).Name, this);

            if (SingleParameter == null)
            {
                _sortedParameter.Clear();

                foreach (var i in Parameter)
                {
                    i.Value.Check(context);

                    // also check if the member is available
                    if (type.Variables.All(v => v.Name != i.Key))
                    {
                        // TODO: add a check
                    }
                }
            }
            else if (type.Variables.Count != 1)
            {
                throw new PositionedException(this, $"Could not create an instance of the class {Type} " +
                                                    $"with the single constructor parameter {SingleParameter.GetResultType(context)}, " +
                                                    $"because the class has {type.Variables.Count} member variables");
            }
        }

        public override bool Print(PrintType type, Printer printer)
        {
            Type.Print(type, printer);

            printer.Write("{");

            if (SingleParameter != null)
            {
                SingleParameter.Print(type, printer);
            }
            else
            {
                foreach (var i in Parameter)
                {
                    if (i.Value.Print(type, printer) && !Equals(i, Parameter.Last()))
                    {
                        printer.Write(", ");
                    }
                }
            }

            printer.Write("}");

            return true;
        }

        public override object Clone()
        {
            return new New
            {
                Type = (SimpleType)Type.Clone(),
                SingleParameter = (IExpression)SingleParameter?.Clone(),
                Parameter = Parameter?.Select(i => (i.Key, (IExpression)i.Value.Clone())).ToDictionary(x => x.Key, x => x.Item2)
            };
        }

        public override string ToString()
        {
            return $"new {Type}({SingleParameter.ToString() ?? string.Join(", ", Parameter.Select(i => $"{i.Key} = {i.Value}"))})";
        }
    }
}