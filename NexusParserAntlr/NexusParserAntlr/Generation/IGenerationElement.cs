namespace NexusParserAntlr.Generation
{
    public interface IGenerationElement : ICheckable
    {
        IGenerationElement Generate(Context context);
    }

    public static class GenerationElementExtensions
    {
        public static T Generate<T>(this IGenerationElement element, Context context) where T : class
        {
            return (T)element.Generate(context);
        }
    }
}