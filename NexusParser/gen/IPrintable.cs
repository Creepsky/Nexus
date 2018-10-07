namespace Nexus.gen
{
    public enum PrintType
    {
        Header,
        Source,
        Parameter
    }

    public interface IPrintable
    {
        void Print(PrintType type, Printer printer);
    }
}