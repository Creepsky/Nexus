using System.Collections.Generic;
using System.Linq;

namespace NexusParserAntlr.Generation
{
    public class Function : IGenerationElement
    {
        public readonly Type Type;
        public readonly IList<Variable> Parameter;
        private readonly ir.Function _function;

        public Function(ir.Function function)
        {
            _function = function;
            Type = new Type(function.Type);
            Parameter = function.Parameter.Select(i => new Variable(i)).ToList();
        }

        public void ToHeader(Printer printer)
        {
            Type.Print(printer);

            printer.Write($" {_function.Name}(");

            if (_function.Parameter.Any())
            {
                var last = _function.Parameter.Last();

                foreach (var i in _function.Parameter)
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
            Type.Print(printer);
            //printer.Write($" {((Class)Context.Element).Name}::{_function.Name}");
            printer.WriteLine(string.Join(", ", _function.Parameter.Select(i => $"{i.Type.ToCpp()} {i.Name}")));
            printer.WriteLine("{ }");
        }

        public void Check(Context upperContext)
        {
            var context = upperContext.StackNewContext(this);

            Type.Check(context);

            foreach (var i in Parameter)
                i.Check(context);
        }

        public IGenerationElement Generate(Context upperContext)
        {
            var context = upperContext.StackNewContext(this);

            Type.Generate(context);

            foreach (var i in Parameter)
                i.Generate(context);

            return this;
        }
    }
}