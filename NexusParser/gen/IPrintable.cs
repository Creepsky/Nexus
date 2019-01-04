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
        Definition
    }

    public interface IPrintable
    {
        bool Print(PrintType type, Printer printer);
    }
}