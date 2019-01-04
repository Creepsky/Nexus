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

        public override SimpleType GetResultType(Context context) =>
            new SimpleType("map",
                new TemplateList(new List<SimpleType>
                    {Values.First().Key.GetResultType(context), Values.First().Value.GetResultType(context)}), 0)
            {
                FilePath = FilePath,
                Line = Line,
                Column = Column
            };

        public override void Check(Context context)
        {
            throw new NotImplementedException();
        }

        public override bool Print(PrintType type, Printer printer)
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
            return true;
        }

        public override object Clone()
        {
            return new MapLiteral
            {
                Values = Values.ToDictionary(i => (IExpression) i.Key.CloneDeep(), i => (IExpression) i.Value.CloneDeep())
            };
        }
    }
}