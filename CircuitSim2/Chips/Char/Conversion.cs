using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Char.Conversion
{
    public sealed class ToByte : UnaryFunctor<char, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base("ByteToChar", a => (byte)a, Engine)
        {

        }
    }

    public sealed class ToInteger : UnaryFunctor<char, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base("CharToInteger", a => a, Engine)
        {

        }
    }

    public sealed class ToLong : UnaryFunctor<char, long>
    {
        public ToLong(Engine.Engine Engine = null) : base("CharToLong", a => a, Engine)
        {

        }
    }
    public sealed class ToString : UnaryFunctor<char, string>
    {
        public ToString(Engine.Engine Engine = null) : base("CharToString", a => a.ToString(), Engine)
        {

        }
    }
}
