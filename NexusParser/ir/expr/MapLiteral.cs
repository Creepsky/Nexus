using System;
using System.Collections.Generic;
using System.Linq;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class MapLiteral : Expression
    {
        public IDictionary<IExpression, IExpression> Values { get; set; }

        public override string ToString()
        {
            return '{' + string.Join(", ", Values.Select(i => '{' + i.Key.ToString() + ", " + i.Value.ToString() + '}')) + '}';
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context) =>
            new MapType
            {
                Line = Line,
                Column = Column,
                KeyType = Values.First().Key.GetResultType(context),
                ValueType = Values.First().Value.GetResultType(context)
            };

        public override void Check(Context context)
        {
            throw new NotImplementedException();
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write("{");
            foreach (var i in Values)
            {
                printer.Write("{");
                i.Key.Print(type, printer);
                printer.Write(", ");
                i.Value.Print(type, printer);
                printer.Write("}");
                if (!i.Equals(Values.Last()))
                {
                    printer.Write(", ");
                }
            }
            printer.Write("}");
        }
    }
}