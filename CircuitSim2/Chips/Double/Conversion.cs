using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Double.Conversion
{
    [PureChip("DoubleToByte")]
    public sealed class ToByte : UnaryFunctor<double, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base(a => (byte)a, Engine)
        {

        }
    }

    [PureChip("DoubleToChar")]
    public sealed class ToChar : UnaryFunctor<double, char>
    {
        public ToChar(Engine.Engine Engine = null) : base(a => (char)a, Engine)
        {

        }
    }

    [PureChip("DoubleToInteger")]
    public sealed class ToInteger : UnaryFunctor<double, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base(a => (int)a, Engine)
        {

        }
    }

    [PureChip("DoubleToLong")]
    public sealed class ToLong : UnaryFunctor<double, long>
    {
        public ToLong(Engine.Engine Engine = null) : base(a => (long)a, Engine)
        {

        }
    }

    [PureChip("DoubleToSingle")]
    public sealed class ToSingle : UnaryFunctor<double, float>
    {
        public ToSingle(Engine.Engine Engine = null) : base(a => (float)a, Engine)
        {

        }
    }

    [PureChip("DoubleToString")]
    public sealed class ToString : UnaryFunctor<double, string>
    {
        public ToString(Engine.Engine Engine = null) : base(a => a.ToString(), Engine)
        {

        }
    }
}
