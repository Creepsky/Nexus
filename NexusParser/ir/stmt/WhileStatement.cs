using System.Collections.Generic;
using System.Linq;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class WhileStatement : Statement
    {
        public IExpression Condition { get; set; }
        public IList<IStatement> Body { get; set; }

        public override void Check(Context context)
        {
            // TODO
        }

        public override SimpleType GetResultType(Context context) =>
            new SimpleType(TypesExtension.Void)
            {
                Line = Line,
                Column = Column
            };

        public override bool Print(PrintType type, Printer printer)
        {
            printer.Write("while (");
            Condition.Print(type, printer);
            printer.WriteLine(")");
            printer.WriteLine("{");
            printer.Push();
            foreach (var i in Body)
            {
                i.Print(PrintType.FunctionSource, printer);
            }
            printer.Pop();
            printer.WriteLine("}");
            return true;
        }

        public override object Clone()
        {
            var condition = (IExpression) Condition.CloneDeep();
            var body = new List<IStatement>(Body.Select(i => (IStatement) i.CloneDeep()));

            return new WhileStatement
            {
                Condition = condition,
                Body = body
            };
        }
    }
}