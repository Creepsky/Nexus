using NexusParserAntlr.ir;

namespace NexusParserAntlr.Generation
{
    public class Type : IGenerationElement
    {
        private readonly IType _astType;

        public Type(IType type)
        {
            _astType = type;
        }

        public void Print(Printer printer)
        {
            printer.Write(_astType.IsPrimitive()
                ? _astType.ToCpp()
                : $"const {_astType.ToCpp()}&");
        }

        public IGenerationElement Generate(Context context)
        {
            return this;
        }

        public void Check(Context context)
        {
            _astType.Check(context);
        }
    }
}