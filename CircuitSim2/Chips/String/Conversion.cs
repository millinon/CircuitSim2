using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.String.Conversion
{
    [Chip("StringToByte")]
    public sealed class ToByte : UnaryFunctor<string, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base(byte.Parse, Engine)
        {

        }
    }

    [Chip("StringToInteger")]
    public sealed class ToInteger : UnaryFunctor<string, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base(int.Parse, Engine)
        {

        }
    }

    [Chip("StringToSingle")]
    public sealed class ToSingle : UnaryFunctor<string, float>
    {
        public ToSingle(Engine.Engine Engine = null) : base(float.Parse, Engine)
        {

        }
    }

    [Chip("StringToDouble")]
    public sealed class ToDouble : UnaryFunctor<string, double>
    {
        public ToDouble(Engine.Engine Engine = null) : base(double.Parse, Engine)
        {

        }
    }
}
