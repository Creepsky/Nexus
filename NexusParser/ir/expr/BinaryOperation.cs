using System;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public enum BinaryOperatorType
    {
        Add,
        Sub,
        Mul,
        Div
    }

    public class BinaryOperation : Expression
    {
        public IExpression Left { get; set; }
        public BinaryOperatorType Type { get; set; }
        public IExpression Right { get; set; }

        public override string ToString()
        {
            var op = GetOperator();

            return $"{Left} {op} {Right}";
        }

        private char GetOperator()
        {
            char op;

            if (Type == BinaryOperatorType.Add)
            {
                op = '+';
            }
            else if (Type == BinaryOperatorType.Div)
            {
                op = '/';
            }
            else if (Type == BinaryOperatorType.Sub)
            {
                op = '-';
            }
            else if (Type == BinaryOperatorType.Mul)
            {
                op = '*';
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(Type), Type, "unknown binary type");
            }
            
            return op;
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context)
        {
            return Left.GetResultType(context);
        }

        public override void Check(Context context)
        {
            // TODO: add checking
        }

        public override void Print(PrintType type, Printer printer)
        {
            Left.Print(type, printer);
            printer.Write($" {GetOperator()} ");
            Right.Print(type, printer);
        }
    }
}