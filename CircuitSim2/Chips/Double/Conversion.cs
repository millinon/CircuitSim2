using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Double.Conversion
{
    public sealed class ToByte : UnaryFunctor<double, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base("DoubleToByte", a => (byte) a, Engine)
        {

        }
    }

    public sealed class ToChar : UnaryFunctor<double, char>
    {
        public ToChar(Engine.Engine Engine = null) : base("DoubleToChar", a => (char) a, Engine)
        {

        }
    }

    public sealed class ToInteger : UnaryFunctor<double, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base("DoubleToInteger", a => (int) a, Engine)
        {

        }
    }

    public sealed class ToLong : UnaryFunctor<double, long>
    {
        public ToLong(Engine.Engine Engine = null) : base("DoubleToLong", a => (long)a, Engine)
        {

        }
    }

    public sealed class ToSingle : UnaryFunctor<double, float>
    {
        public ToSingle(Engine.Engine Engine = null) : base("DoubleToSingle", a => (float) a, Engine)
        {

        }
    }

    public sealed class ToString : UnaryFunctor<double, string>
    {
        public ToString(Engine.Engine Engine = null) : base("DoubleToString", a => a.ToString(), Engine)
        {

        }
    }
}
