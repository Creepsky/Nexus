using System;
using Nexus.common;
using Nexus.gen;

namespace Nexus.ir.expr
{
    public class ArrayAccess : Expression
    {
        public IExpression Index { get; set; }
        
        public override string ToString() => $"{Name}[{Index}]";

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context)
        {
            throw new NotImplementedException();
        }

        public override void Check(Context context)
        {
            var symbol = context.Get(Name);

            if (symbol == null)
            {
                throw new NotFoundException(this, "element", Name);
            }
            
            var resultType = symbol.GetResultType(context);

            if (resultType.GetType() == typeof(SimpleType))
            {
                var simpleType = (SimpleType) resultType;

                if (simpleType.Array == 0)
                {
                    if (simpleType.IsPrimitive())
                    {
                        var primitiveType = simpleType.ToPrimitiveType();

                        // from primitives, only strings can be indexed
                        if (primitiveType != PrimitiveType.String)
                        {
                            throw new TypeMismatchException(Index, PrimitiveType.String.ToString(), nameof(primitiveType));
                        }
                    }
                    else
                    {
                        // TODO: check if class has "at" function
                        throw new NotImplementedException();
                    }
                }
            }
            else if (resultType.GetType() == typeof(MapType))
            {
                var map = (MapType) resultType;
                map.KeyType.GetResultType(context);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override void Print(PrintType type, Printer printer)
        {
            printer.Write($"{Name}[");
            Index.Print(type, printer);
            printer.Write("]");
        }
    }
}