using System.Collections.Generic;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class Variable : Expression
    {
        public SimpleType Type { get; set; }
        //public SimpleType Type { get; set; }
        //public IExpression Initialization { get; set; }
        //public bool Constant { get; set; }
        //public bool Reference { get; set; }
        //public bool Parameter { get; set; }
        private Context _context;

        public override string ToString()
        {
            return $"{Type} {Name}";
        }

        public override void Check(Context upperContext)
        {
            Type.Check(upperContext);
           //Initialization?.Check(upperContext);
        }

        public override SimpleType GetResultType(Context context) => Type;

        public override void ForwardDeclare(Context upperContext)
        {
            _context = upperContext;
            Type.ForwardDeclare(_context);

            if (_context.Element is Class ||
                _context.Element is Function ||
                _context.Element is ForStatement)
            {
                _context.Add(Name, this);
            }
            else
            {
                throw new UnexpectedScopeException(this, _context.Element.GetType().Name,
                    new List<string>{nameof(Class), nameof(Function)});
            }
        }

        public override void Declare()
        {
        }

        public override bool Print(PrintType type, Printer printer)
        {
            //var isCppType = Type is CppType;

            //if (Constant && !isCppType)
            //{
            //    printer.Write("const ");
            //}

            Type.Print(type, printer);

            //if (Reference && !isCppType)
            //{
            //    printer.Write("&");
            //}

            if (type != PrintType.ForwardDeclaration)
            {
                printer.Write($" {Name}");
            }

            //if (Initialization != null)
            //{
            //    printer.Write(" = ");
            //    Initialization?.Print(type, printer);
            //}

            //if (!Parameter)
            //{
            //    printer.WriteLine(";");
            //}

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