using System.Collections.Generic;
using Nexus.common;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class ArrayAccess : Expression
    {
        public IExpression Value { get; set; }
        public IExpression Index { get; set; }
        
        public override string ToString() => $"{Value.Name}[{Index}]";

        //public override IGenerationElement Generate(Context context, GenerationPhase phase)
        //{
        //    Value.Generate(context, phase);
        //    Index.Generate(context, phase);
        //    return this;
        //}

        public override SimpleType GetResultType(Context context)
        {
            return GetFunctionCall().GetResultType(context);
        }

        public override void Check(Context context)
        {
            var valueType = Value.GetResultType(context);
            var indexType = Index.GetResultType(context);
            var operatorName = Function.OperatorToName("[]");
            var op = context.Get<Function>(operatorName, this);
            var opOverloading = op.GetOverload(GetFunctionCall(), context);
            
            if (opOverloading == null)
            {
                throw new OverloadingFunctionNotFound(this, valueType.ToString(), "operator[]", new []{indexType.ToString()});
            }
        }

        private FunctionCall GetFunctionCall()
        {
            return new FunctionCall{
                Column = Column,
                FilePath = FilePath,
                Line = Line,
                Name = Function.OperatorToName("[]"),
                Parameter = new List<IExpression>{Index},
                Object = Value
            };
        }

        public override bool Print(PrintType type, Printer printer)
        {
            printer.Write($"{Function.OperatorToName("[]")}(");
            Value.Print(type, printer);
            printer.Write(", ");
            Index.Print(type, printer);
            printer.Write(")");
            return true;
        }

        public override object Clone()
        {
            return new ArrayAccess
            {
                Value = (IExpression) Value.CloneDeep(),
                Index = (IExpression) Index.CloneDeep()
            };
        }
    }
}