using System.Collections.Generic;
using System.Linq;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class FunctionCall : Expression
    {
        public IExpression Object { get; set; }
        public IList<IExpression> Parameter { get; set; }
        public bool Static { get; set; }
        private Function _function;

        private Function GetOverload(Context context)
        {
            if (_function == null)
            {
                if (context == null)
                {
                    throw new OverloadingFunctionNotFound(this, Name, Parameter.Select(i => i.ToString()).ToArray());
                }

                var function = context.Get<Function>(Name, this);
                _function = function.GetOverload(this, context);

                if (_function == null)
                {
                    if (Object != null)
                    {
                        throw new OverloadingFunctionNotFound(this, Object.GetResultType(context).ToString(), Name,
                            Parameter.Select(i => i.GetResultType(context).ToString()).ToArray());
                    }

                    throw new OverloadingFunctionNotFound(this, Name, Parameter.Select(i => i.GetResultType(context).ToString()).ToArray());
                }
            }

            return _function;
        }

        public override string ToString()
        {
            var name = $"{Name}({string.Join(", ", Parameter)})";
            if (Object != null)
            {
                name = Object.ToString() + '.' + name;
            }
            name += ")";
            return name;
        }

        public override SimpleType GetResultType(Context context)
        {
            return GetOverload(context).ReturnType;
        }

        public override void ForwardDeclare(Context upperContext)
        {
            Object?.ForwardDeclare(upperContext);
            foreach (var i in Parameter)
            {
                i.ForwardDeclare(upperContext);
            }
        }

        public override void Declare()
        {
            Object?.Declare();
            foreach (var i in Parameter)
            {
                i.Declare();
            }
        }

        public override void Define()
        {
            Object?.Define();
            foreach (var i in Parameter)
            {
                i.Define();
            }
        }

        public override void Remove()
        {
            Object?.Remove();
            foreach (var i in Parameter)
            {
                i.Remove();
            }
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            Object?.Template(context, concreteElement);
            foreach (var i in Parameter)
            {
                i.Template(context, concreteElement);
            }
        }

        public override void Check(Context context)
        {
            if (Static)
            {
                context.Get<Class>(Object.Name, this);
            }
            else
            {
                Object?.Check(context);
            }

            // if the extension type is a c++ type, we can't ensure nothing.. so just return as it is ok
            if (Object?.GetResultType(context) is CppType)
            {
                return;
            }
            
            if (GetOverload(context) != null)
            {
                // everything's ok
                return;
            }

            if (Object != null)
            {
                throw new OverloadingFunctionNotFound(this,
                    Object.GetResultType(context).ToString(), Name,
                    Parameter.Select(i => i.GetResultType(context).ToString()).ToArray());
            }

            throw new OverloadingFunctionNotFound(this,
                Name,
                Parameter.Select(i => i.GetResultType(context).ToString()).ToArray());
        }

        public override bool Print(PrintType type, Printer printer)
        {
            if (Static)
            {
                Object.Print(type, printer);
                printer.Write("_");
            }
            printer.Write(Name);
            printer.Write("(");
            if (Object != null)
            {
                if (!Static)
                {
                    Object.Print(type, printer);
                    if (Parameter.Any())
                    {
                        printer.Write(", ");
                    }
                }
            }
            foreach (var i in Parameter)
            {
                i.Print(type, printer);
                if (!i.Equals(Parameter.Last()))
                {
                    printer.Write(", ");
                }
            }
            printer.Write(")");
            return true;
        }

        public override object Clone()
        {
            return new FunctionCall
            {
                Object = Object?.CloneDeep() as IExpression,
                Parameter = Parameter.Select(i => (IExpression) i.CloneDeep()).ToList(),
                Static = Static
            };
        }
    }
}