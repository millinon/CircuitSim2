using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Long.Conversion
{
    public sealed class ToByte : UnaryFunctor<long, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base("LongToByte", a => (byte) a, Engine)
        {

        }
    }

    public sealed class ToChar : UnaryFunctor<long, char>
    {
        public ToChar(Engine.Engine Engine = null) : base("LongToChar", a => (char) a, Engine)
        {

        }
    }

    public sealed class ToInteger : UnaryFunctor<long, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base("LongToInteger", a => (int) a, Engine)
        {

        }
    }

    public sealed class ToSingle : UnaryFunctor<long, float>
    {
        public ToSingle(Engine.Engine Engine = null) : base("LongToSingle", a => a, Engine)
        {

        }
    }

    public sealed class ToDouble : UnaryFunctor<long, double>
    {
        public ToDouble(Engine.Engine Engine = null) : base("LongToDouble", a => a, Engine)
        {

        }
    }

    public sealed class ToString : UnaryFunctor<long, string>
    {
        public ToString(Engine.Engine Engine = null) : base("LongToString", a => a.ToString(), Engine)
        {

        }
    }
}
