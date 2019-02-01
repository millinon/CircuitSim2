using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Single.Conversion
{
    [PureChip("SingleToByte")]
    public sealed class ToByte : UnaryFunctor<float, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base(a => (byte)a, Engine)
        {

        }
    }

    [PureChip("SingleToChar")]
    public sealed class ToChar : UnaryFunctor<float, char>
    {
        public ToChar(Engine.Engine Engine = null) : base(a => (char)a, Engine)
        {

        }
    }

    [PureChip("SingleToInteger")]
    public sealed class ToInteger : UnaryFunctor<float, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base(a => (int)a, Engine)
        {

        }
    }

    [PureChip("SingleToLong")]
    public sealed class ToLong : UnaryFunctor<float, long>
    {
        public ToLong(Engine.Engine Engine = null) : base(a => (long)a, Engine)
        {

        }
    }

    [PureChip("SingleToDouble")]
    public sealed class ToDouble : UnaryFunctor<float, double>
    {
        public ToDouble(Engine.Engine Engine = null) : base(a => a, Engine)
        {

        }
    }

    [PureChip("SingleToString")]
    public sealed class ToString : UnaryFunctor<float, string>
    {
        public ToString(Engine.Engine Engine = null) : base(a => a.ToString(), Engine)
        {

        }
    }
}
