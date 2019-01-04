using Nexus.common;
using Nexus.gen;
using Nexus.ir.expr;

namespace Nexus.ir.stmt
{
    public class Include : Statement
    {
        public string Path { get; }

        public Include(string path)
        {
            Path = path;
        }

        public override void Check(Context context)
        {
            var scopeType = context.Element.GetType();

            if (scopeType != typeof(Class) &&
                scopeType != typeof(File) &&
                scopeType != typeof(Function))
            {
                throw new UnexpectedScopeException(this, scopeType.Name, new []
                {
                    nameof(File), nameof(Class), nameof(Function)
                });
            }
        }

        public override SimpleType GetResultType(Context context)
        {
            throw new UnexpectedCallException(this, GetType().Name, "GetResultType");
        }

        public override void ForwardDeclare(Context upperContext)
        {
            File file;

            if (upperContext.Element.GetType() == typeof(Class) ||
                upperContext.Element.GetType() == typeof(Function))
            {
                file = upperContext.UpperContext.GetElementAs<File>(this);
            }
            else if (upperContext.Element.GetType() == typeof(File))
            {
                file = upperContext.GetElementAs<File>(this);
            }
            else
            {
                throw new PositionedException(this, "Could not extract file from context");
            }

            file.Add(this);
        }

        public override bool Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                printer.WriteLine($"#include <{Path}>");
                return true;
            }
            return false;
        }

        public override object Clone()
        {
            return new Include(new string(Path));
        }
    }
}