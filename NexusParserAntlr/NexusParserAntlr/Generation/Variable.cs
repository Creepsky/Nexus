namespace NexusParserAntlr.Generation
{
    public class Variable : IGenerationElement
    {
        public readonly Type Type;

        public Variable(ir.Variable variable)
        {
            Type = new Type(variable.Type);
        }

        public void Check(Context context)
        {
            Type.Check(context);
        }

        public IGenerationElement Generate(Context context)
        {
            Type.Generate(context);
            return this;
        }
    }
}