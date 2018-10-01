namespace NexusParserAntlr.ir
{
    public class Variable : IStatement
    {
        public IType Type;
        public string Name;
        public bool Setter;
        public bool Getter;
        public IExpression Initialization;

        public override string ToString()
        {
            return $"{Type} {Name}";
        }
    }
}