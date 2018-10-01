using System.Collections.Generic;

namespace NexusParserAntlr.ir
{
    public class Function : IStatement
    {
        public IType Type;
        public string Name;
        public IList<IStatement> Statements;

        public override string ToString()
        {
            return $"{Type} {Name}()";
        }
    }
}