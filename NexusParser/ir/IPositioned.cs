namespace Nexus.ir
{
    public interface IPositioned
    {
        int Line { set; get; }
        int Column { set; get; }
    }
}