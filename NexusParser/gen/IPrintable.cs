namespace Nexus.gen
{
    public enum PrintType
    {
        Header,
        Source,
        Parameter,
        ParameterRef,
        ParameterConstRef
    }

    public interface IPrintable
    {
        void Print(PrintType type, Printer printer);
    }
}