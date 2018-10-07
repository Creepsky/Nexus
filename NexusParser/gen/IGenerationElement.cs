using Nexus.ir;

namespace Nexus.gen
{
    public enum GenerationPhase
    {
        ForwardDeclaration,
        Declaration,
        Definition
    }

    public interface IGenerationElement : ICheckable, IPrintable, IPositioned
    {
        IGenerationElement Generate(Context context, GenerationPhase phase);
    }

    public static class GenerationElementExtensions
    {
        public static T Generate<T>(this IGenerationElement element, Context context, GenerationPhase phase) where T : class
        {
            return (T)element.Generate(context, phase);
        }
    }
}