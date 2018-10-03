using NexusParserAntlr.ir;

namespace NexusParserAntlr.Generation
{
    public class CompilationVariable
    {
        public readonly CompilationType Type;

        public CompilationVariable(CompilationUnit unit, Variable variable)
        {
            Type = new CompilationType(unit, variable.Type);
        }
    }
}