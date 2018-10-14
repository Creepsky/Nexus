using System.Collections.Generic;
using System.Linq;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class TupleExplosionStatement : Statement
    {
        public IList<string> Names { get; set; }
        public IExpression Right { get; set; }

        public override void Check(Context context)
        {
            if (Right.GetType() == typeof(TupleLiteral))
            {
            }
            else if (Right.GetType() == typeof(FunctionCall))
            {
                var functionCall = (FunctionCall)Right;
                context.Get<Function>(functionCall.Name, Right);
            }
            else
            {
                throw new TypeMismatchException(Right, new[] {nameof(TupleLiteral), nameof(FunctionCall)},
                    Right.GetType().Name);
            }
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            foreach (var i in Names.Where(i => i != "_"))
                context.Add(i, new Variable
                {
                    Type = Right.GetResultType(context),
                    Name = i,
                });

            return this;
        }

        public override IType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void, Line, Column);

        public override void Print(PrintType type, Printer printer)
        {
            // TODO
        }
    }
}