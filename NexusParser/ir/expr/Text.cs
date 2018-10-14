using System;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class Text : Expression
    {
        public string Value { get; set; }

        public override string ToString() => '"' + Value + '"';

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context) =>
            new SimpleType
            {
                Line = Line,
                Column = Column,
                Name = "string"
            };

        public override void Check(Context context)
        {
            // nothing to check yet
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write('"' + Value + '"');

        }
    }
}