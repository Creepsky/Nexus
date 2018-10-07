using System.Collections.Generic;
using System.Linq;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class Function : Statement
    {
        public IType Type;
        public string Name;
        public IList<Variable> Parameter;
        public IList<IStatement> Statements;

        public override string ToString()
        {
            return $"{Type} {Name}({string.Join(',', Parameter.Select(i => i.Type))})";
        }

        public void ToHeader(Printer printer)
        {
            Type.Print(PrintType.Header, printer);

            printer.Write($" {Name}(");

            if (Parameter.Any())
            {
                var last = Parameter.Last();

                foreach (var i in Parameter)
                {
                    printer.Write(i.Type.IsPrimitive() ? $"{i.Type.ToCpp()}" : $"const {i.Type.ToCpp()}&");
                    printer.Write($" {i.Name}");

                    if (last != i)
                        printer.Write(", ");
                }
            }

            printer.WriteLine(");");
        }

        public void ToSource(Printer printer)
        {
            Type.Print(PrintType.Source, printer);
            //printer.Write($" {((Class)Context.Element).Name}::{_function.Name}");
            printer.WriteLine(string.Join(", ", Parameter.Select(i => $"{i.Type.ToCpp()} {i.Name}")));
            printer.WriteLine("{ }");
        }

        public override void Check(Context upperContext)
        {
            var context = upperContext.StackNewContext(this);

            Type.Check(context);

            foreach (var i in Parameter)
                i.Check(context);

            foreach (var i in Statements)
                i.Check(context);
        }

        public override IGenerationElement Generate(Context upperContext)
        {
            var context = upperContext.StackNewContext(this);

            Type.Generate(context);

            foreach (var i in Parameter)
                i.Generate(context);

            return this;
        }

        public override void Print(PrintType type, Printer printer)
        {
            throw new System.NotImplementedException();
        }
    }
}