﻿using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class VariableLiteral : Expression
    {
        public override string ToString() => Name;

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context)
        {
            return context.Get(Name, this).GetResultType(context);
        }

        public override void Check(Context context)
        {
            if (Name == "this")
            {
                context.GetElementAs<ExtensionFunction>(this);
            }
            else
            {
                // does the variable exist in this or some higher context?
                context.Get<Variable>(Name, this);
            }
        }

        public override void Print(PrintType type, Printer printer)
        {
            if (Name == "this")
            {
                printer.Write("__this");
            }
            else
            {
                printer.Write(Name);
            }
        }
    }
}