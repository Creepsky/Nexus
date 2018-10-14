using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class TupleLiteral : Expression
    {
        public IList<IExpression> Values { get; set; }

        public override string ToString() => $"std::make_tuple({string.Join(", ", Values)})";

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context) =>
            new TupleType
            {
                Line = Line,
                Column = Column,
                Types = Values.Select(i => GetResultType(context)).ToList()
            };

        public override void Check(Context context)
        {
            foreach (var i in Values)
                i.Check(context);
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write("std::make_tuple(");
            foreach (var i in Values)
            {
                i.Print(type, printer);
                if (i != Values.Last())
                    printer.Write(", ");
            }
            printer.Write(")");
        }
    }
}