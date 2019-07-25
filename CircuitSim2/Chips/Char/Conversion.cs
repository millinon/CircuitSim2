using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Char.Conversion
{
    [Chip("CharToByte")]
    public sealed class ToByte : UnaryFunctor<char, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base(a => (byte)a, Engine)
        {

        }
    }

    [Chip("CharToInteger")]
    public sealed class ToInteger : UnaryFunctor<char, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base(a => a, Engine)
        {

        }
    }

    [Chip("CharToLong")]
    public sealed class ToLong : UnaryFunctor<char, long>
    {
        public ToLong(Engine.Engine Engine = null) : base(a => a, Engine)
        {

        }
    }
    [Chip("CharToString")]
    public sealed class ToString : UnaryFunctor<char, string>
    {
        public ToString(Engine.Engine Engine = null) : base(a => a.ToString(), Engine)
        {

        }
    }
}
