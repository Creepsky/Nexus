using System;
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
            if (Right.GetType() == typeof(FunctionCall))
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

        public override SimpleType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void)
            {
                Line = Line,
                Column = Column
            };

        public override void ForwardDeclare(Context upperContext)
        {
            foreach (var i in Names.Where(i => i != "_"))
            {
                upperContext.Add(i, new Variable
                {
                    Type = Right.GetResultType(upperContext),
                    Name = i,
                });
            }
        }

        public override bool Print(PrintType type, Printer printer)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            return new TupleExplosionStatement
            {
                Names = Names.Select(i => new string(i)).ToList(),
                Right = (IExpression) Right.CloneDeep()
            };
        }
    }
}