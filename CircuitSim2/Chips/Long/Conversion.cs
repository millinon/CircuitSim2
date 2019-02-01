using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Long.Conversion
{
    [PureChip("LongToByte")]
    public sealed class ToByte : UnaryFunctor<long, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base(a => (byte)a, Engine)
        {

        }
    }

    [PureChip("LongToChar")]
    public sealed class ToChar : UnaryFunctor<long, char>
    {
        public ToChar(Engine.Engine Engine = null) : base(a => (char)a, Engine)
        {

        }
    }

    [PureChip("LongToInteger")]
    public sealed class ToInteger : UnaryFunctor<long, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base(a => (int)a, Engine)
        {

        }
    }

    [PureChip("LongToSingle")]
    public sealed class ToSingle : UnaryFunctor<long, float>
    {
        public ToSingle(Engine.Engine Engine = null) : base(a => a, Engine)
        {

        }
    }

    [PureChip("LongToDouble")]
    public sealed class ToDouble : UnaryFunctor<long, double>
    {
        public ToDouble(Engine.Engine Engine = null) : base(a => a, Engine)
        {

        }
    }

    [PureChip("LongToString")]
    public sealed class ToString : UnaryFunctor<long, string>
    {
        public ToString(Engine.Engine Engine = null) : base(a => a.ToString(), Engine)
        {

        }
    }
}
