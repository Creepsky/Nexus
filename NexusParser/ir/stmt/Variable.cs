using System.Collections.Generic;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class Variable : Expression
    {
        public SimpleType Type { get; set; }
        public IExpression Initialization { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name}";
        }

        public override void Check(Context upperContext)
        {
            Type.Check(upperContext);
            if (Initialization != null)
            {
                if (!(Initialization is Assignment))
                {
                    throw new PositionedException(this, $"Expected initialization of variable {this} " +
                                                        $"to be {typeof(Assignment).Name} but got {Initialization.GetType().Name}");
                }
                Initialization.Check(upperContext);
            }
        }

        public override SimpleType GetResultType(Context context) => Type;

        public override void ForwardDeclare(Context upperContext)
        {
            Type.ForwardDeclare(upperContext);

            if (upperContext.Element is Class ||
                upperContext.Element is Function ||
                upperContext.Element is ForStatement)
            {
                upperContext.Add(Name, this);
            }
            else
            {
                throw new UnexpectedScopeException(this, upperContext.Element.GetType().Name,
                    new List<string>{nameof(Class), nameof(Function)});
            }
        }
        
        public override bool Print(PrintType type, Printer printer)
        {
            Type.Print(type, printer);

            if (type != PrintType.ForwardDeclaration)
            {
                printer.Write($" {Name}");
            }

            return true;
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            Type.Template(context, concreteElement);
            ForwardDeclare(context);
        }

        public override object Clone()
        {
            return new Variable
            {
                Type = (SimpleType) Type.CloneDeep(),
                //Initialization = (IExpression) Initialization?.CloneDeep(),
                //Constant = Constant,
                //Reference = Reference,
                //Parameter = Parameter
            };
        }
    }
}