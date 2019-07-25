using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Double.Conversion
{
    [Chip("DoubleToByte")]
    public sealed class ToByte : UnaryFunctor<double, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base(a => (byte)a, Engine)
        {

        }
    }

    [Chip("DoubleToChar")]
    public sealed class ToChar : UnaryFunctor<double, char>
    {
        public ToChar(Engine.Engine Engine = null) : base(a => (char)a, Engine)
        {

        }
    }

    [Chip("DoubleToInteger")]
    public sealed class ToInteger : UnaryFunctor<double, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base(a => (int)a, Engine)
        {

        }
    }

    [Chip("DoubleToLong")]
    public sealed class ToLong : UnaryFunctor<double, long>
    {
        public ToLong(Engine.Engine Engine = null) : base(a => (long)a, Engine)
        {

        }
    }

    [Chip("DoubleToSingle")]
    public sealed class ToSingle : UnaryFunctor<double, float>
    {
        public ToSingle(Engine.Engine Engine = null) : base(a => (float)a, Engine)
        {

        }
    }

    [Chip("DoubleToString")]
    public sealed class ToString : UnaryFunctor<double, string>
    {
        public ToString(Engine.Engine Engine = null) : base(a => a.ToString(), Engine)
        {

        }
    }
}
