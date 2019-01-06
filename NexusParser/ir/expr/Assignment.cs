using System.Collections.Generic;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class Assignment : Expression
    {
        public IExpression Left { private get; set; }
        public IExpression Right { private get; set; }
        private SimpleType _resultType;
        private FunctionCall _functionCall;

        public Assignment(IExpression left, IExpression right)
        {
            Left = left;
            Right = right;
        }

        public override SimpleType GetResultType(Context context)
        {
            return _resultType ?? (_resultType = context.Get<Function>(Function.OperatorToName("="), this)
                       .GetOverload(GetFunctionCall(), context)
                       .GetResultType(context));
        }

        public override void Check(Context context)
        {
            Left.Check(context);
            Right.Check(context);
            GetFunctionCall().Check(context);
        }

        public override bool Print(PrintType type, Printer printer)
        {
            if (Left is Variable)
            {
                Left.Print(type, printer);
                printer.WriteLine(";");
            }

            printer.Write($"assign({Left.Name}, ");
            Right.Print(type, printer);
            printer.Write(")");
            return true;
        }

        public override object Clone()
        {
            return new Assignment((IExpression)Left.CloneDeep(), (IExpression)Right.CloneDeep());
        }

        public override void ForwardDeclare(Context upperContext)
        {
            Left.ForwardDeclare(upperContext);
            Right.ForwardDeclare(upperContext);
        }

        public override string ToString()
        {
            return $"{Left} = {Right}";
        }

        private FunctionCall GetFunctionCall() =>
            _functionCall ?? (_functionCall = new FunctionCall
            {
                Column = Column,
                Line = Line,
                FilePath = FilePath,
                Name = Function.OperatorToName("="),
                Object = Left,
                Parameter = new List<IExpression> {Right},
                Static = false
            });
    }
}