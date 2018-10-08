namespace Nexus.gen
{
    public enum PrintType
    {
        Header,
        Source,
        FunctionSource,
        Parameter,
        ParameterRef,
        ParameterConstRef,
        ForSource
    }

    public interface IPrintable
    {
        void Print(PrintType type, Printer printer);
    }
}