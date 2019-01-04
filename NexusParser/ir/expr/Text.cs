using System;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class Text : Expression
    {
        public string Value { get; set; }

        public override string ToString() => '"' + Value + '"';

        public override SimpleType GetResultType(Context context) =>
            new SimpleType(TypesExtension.String)
            {
                FilePath = FilePath,
                Line = Line,
                Column = Column
            };

        public override void Check(Context context)
        {
            // nothing to check yet
        }

        public override bool Print(PrintType type, Printer printer)
        {
            printer.Write("string(\"" + Value + "\")");
            return true;
        }

        public override object Clone()
        {
            return new Text
            {
                Value = new string(Value)
            };
        }
    }
}