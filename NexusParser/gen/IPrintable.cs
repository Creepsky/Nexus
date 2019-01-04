namespace Nexus.gen
{
    public enum PrintType
    {
        Header,
        Source,
        ClassDefinition,
        FunctionSource,
        ForSource,
        ForwardDeclaration,
        Declaration,
        Definition,
        WithoutModifiers
    }

    public interface IPrintable
    {
        bool Print(PrintType type, Printer printer);
    }
}