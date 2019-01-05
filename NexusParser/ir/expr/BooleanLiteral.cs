using Nexus.gen;

namespace Nexus.ir.expr
{
    public class BooleanLiteral : Expression
    {
        public bool Value { get; set; }
        private SimpleType _returnType;

        public override string ToString() => Value ? "true" : "false";

        public override SimpleType GetResultType(Context context) =>
            _returnType ?? (_returnType = new SimpleType(TypesExtension.Bool)
            {
                FilePath = FilePath,
                Line = Line,
                Column = Column
            });
        
        public override void Check(Context context)
        {
            // TODO
        }

        public override bool Print(PrintType type, Printer printer)
        {
            printer.Write(Value ? "true" : "false");
            return true;
        }

        public override object Clone()
        {
            return new BooleanLiteral
            {
                Value = Value
            };
        }
    }
}