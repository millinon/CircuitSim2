using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Byte.Conversion
{
    public sealed class ToChar : UnaryFunctor<byte, char>
    {
        public ToChar(Engine.Engine Engine = null) : base("ByteToChar", a => (char) a, Engine)
        {

        }
    }

    public sealed class ToInteger : UnaryFunctor<byte, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base("ByteToInteger", a => a, Engine)
        {

        }
    }

    public sealed class ToLong : UnaryFunctor<byte, long>
    {
        public ToLong(Engine.Engine Engine = null) : base("ByteToLong", a => a, Engine)
        {

        }
    }

    public sealed class ToSingle : UnaryFunctor<byte, float>
    {
        public ToSingle(Engine.Engine Engine = null) : base("ByteToSingle", a => a, Engine)
        {

        }
    }

    public sealed class ToDouble : UnaryFunctor<byte, double>
    {
        public ToDouble(Engine.Engine Engine = null) : base("ByteToDouble", a => a, Engine)
        {

        }
    }

    public sealed class ToString : UnaryFunctor<byte, string>
    {
        public ToString(Engine.Engine Engine = null) : base("ByteToString", a => a.ToString(), Engine)
        {

        }
    }
}
