using System;
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
                scopeType != typeof(ExtensionFunction))
            {
                throw new UnexpectedScopeException(this, scopeType.Name, new []
                {
                    nameof(File), nameof(Class), nameof(ExtensionFunction)
                });
            }
        }

        public override IGenerationElement Generate(Context context, GenerationPhase phase)
        {
            File file;

            if (context.Element.GetType() == typeof(Class) ||
                context.Element.GetType() == typeof(ExtensionFunction))
            {
                file = context.UpperContext.GetElementAs<File>(this);
            }
            else if (context.Element.GetType() == typeof(File))
            {
                file = context.GetElementAs<File>(this);
            }
            else
            {
                throw new PositionedException(this, "Could not extract file from context");
            }

            file.Add(this);

            return this;
        }

        public override IType GetResultType(Context context)
        {
            throw new UnexpectedCallException(this, GetType().Name, "GetResultType");
        }

        public override void Print(PrintType type, Printer printer)
        {
            if (type == PrintType.Header)
            {
                printer.WriteLine($"#include <{Path}>");
            }
        }
    }
}