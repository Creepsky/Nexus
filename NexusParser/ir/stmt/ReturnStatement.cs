using System;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class ReturnStatement : Statement
    {
        public IExpression Value { get; set; }
        private Context _context;

        public override void Check(Context context)
        {
            if (context.Element == null)
            {
                throw new PositionedException(this, "return statement without parent scope");
            }

            Value.Check(context);

            var expectedReturnType = context.Element.GetResultType(context);
            var actualReturnType = Value.GetResultType(context);

            if (!actualReturnType.Equals(expectedReturnType))
            {
                throw new PositionedException(this, $"Expected return type {expectedReturnType} but got {actualReturnType}");
            }
        }

        public override SimpleType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void)
            {
                FilePath = FilePath,
                Line = Line,
                Column = Column
            };

        public override void ForwardDeclare(Context upperContext)
        {
            _context = upperContext;
            Value.ForwardDeclare(upperContext);
        }

        public override void Declare()
        {
            if (_context.Element is Function function && function.TemplateList == null)
            {
                if (Value is VariableLiteral i)
                {
                    // if the same element is returned on which the function was called, the return type needs
                    // to be set to a reference, to prevent a copy
                    if (i.Name == "this")
                    {
                        function.ReturnType.Reference = true;
                    }
                }

                // TODO: somehow use the return type modifier
                //if (Value is FunctionCall f)
                //{
                //    var resultType = f.GetResultType(_context);
                //    function.ReturnType.Constant = resultType.Constant;
                //    function.ReturnType.Reference = resultType.Reference;
                //}
            }

            Value.Declare();
        }

        public override void Define()
        {
            Value.Define();
        }

        public override void Remove()
        {
            Value.Remove();
        }

        public override void Template(TemplateContext context, IGenerationElement concreteElement)
        {
            _context = context;
            Value.Template(context, concreteElement);
        }

        public override bool Print(PrintType type, Printer printer)
        {
            printer.Write("return ");
            Value.Print(type, printer);
            printer.WriteLine(";");
            return true;
        }

        public override object Clone()
        {
            return new ReturnStatement
            {
                Value = (IExpression) Value.CloneDeep()
            };
        }
    }
}