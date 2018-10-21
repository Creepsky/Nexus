using Nexus.ir;
using Nexus.ir.expr;

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
        string Name { get; set; }
        string Path { get; set; }
        IGenerationElement Generate(Context context, GenerationPhase phase);
        IType GetResultType(Context context);
    }

    public static class GenerationElementExtensions
    {
        public static T Generate<T>(this IGenerationElement element, Context context, GenerationPhase phase) where T : class
        {
            return (T)element.Generate(context, phase);
        }
    }
}