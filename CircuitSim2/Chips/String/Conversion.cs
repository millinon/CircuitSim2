using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.String.Conversion
{
    public sealed class ToByte : UnaryFunctor<string, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base("StringToByte", byte.Parse, Engine)
        {

        }
    }

    public sealed class ToInteger : UnaryFunctor<string, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base("StringToInteger", int.Parse, Engine)
        {

        }
    }

    public sealed class ToSingle : UnaryFunctor<string, float>
    {
        public ToSingle(Engine.Engine Engine = null) : base("StringToSingle", float.Parse, Engine)
        {

        }
    }

    public sealed class ToDouble : UnaryFunctor<string, double>
    {
        public ToDouble(Engine.Engine Engine = null) : base("StringToDouble", double.Parse, Engine)
        {

        }
    }
}
