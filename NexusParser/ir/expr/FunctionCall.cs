﻿using System;
using System.Collections.Generic;
using Nexus.gen;
using Nexus.ir.stmt;

namespace Nexus.ir.expr
{
    public class FunctionCall : Expression
    {
        public IList<IExpression> Parameter { get; set; }

        public override string ToString() => $"{Name}({string.Join(", ", Parameter)})";

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            return this;
        }

        public override IType GetResultType(Context context) =>
            ((Function) context.Get(Name)).Type;

        public override void Check(Context context)
        {
            throw new NotImplementedException();
        }

        public override void Print(PrintType type, Printer printer)
        {
            throw new NotImplementedException();
        }
    }
}