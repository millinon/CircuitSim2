using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.String.Conversion
{
    [PureChip("StringToByte")]
    public sealed class ToByte : UnaryFunctor<string, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base(byte.Parse, Engine)
        {

        }
    }

    [PureChip("StringToInteger")]
    public sealed class ToInteger : UnaryFunctor<string, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base(int.Parse, Engine)
        {

        }
    }

    [PureChip("StringToSingle")]
    public sealed class ToSingle : UnaryFunctor<string, float>
    {
        public ToSingle(Engine.Engine Engine = null) : base(float.Parse, Engine)
        {

        }
    }

    [PureChip("StringToDouble")]
    public sealed class ToDouble : UnaryFunctor<string, double>
    {
        public ToDouble(Engine.Engine Engine = null) : base(double.Parse, Engine)
        {

        }
    }
}
