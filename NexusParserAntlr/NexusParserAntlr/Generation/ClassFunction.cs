using System.Linq;
using NexusParserAntlr.ir;

namespace NexusParserAntlr.Generation
{
    public class ClassFunction
    {
        public readonly CompilationUnit Unit;
        public readonly CompilationType Type;
        private readonly Function _function;

        public ClassFunction(CompilationUnit unit, Function function)
        {
            Unit = unit;
            _function = function;
            Type = new CompilationType(unit, function.Type);
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
            printer.Write($" {Unit.Name}::{_function.Name}");
            printer.WriteLine(string.Join(", ", _function.Parameter.Select(i => $"{i.Type.ToCpp()} {i.Name}")));
            printer.WriteLine("{ }");
        }

        public static void Compile(CompilationUnit unit, Function function)
        {
            // TODO: compile
        }
    }
}