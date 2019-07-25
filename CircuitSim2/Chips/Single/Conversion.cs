using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Single.Conversion
{
    [Chip("SingleToByte")]
    public sealed class ToByte : UnaryFunctor<float, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base(a => (byte)a, Engine)
        {

        }
    }

    [Chip("SingleToChar")]
    public sealed class ToChar : UnaryFunctor<float, char>
    {
        public ToChar(Engine.Engine Engine = null) : base(a => (char)a, Engine)
        {

        }
    }

    [Chip("SingleToInteger")]
    public sealed class ToInteger : UnaryFunctor<float, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base(a => (int)a, Engine)
        {

        }
    }

    [Chip("SingleToLong")]
    public sealed class ToLong : UnaryFunctor<float, long>
    {
        public ToLong(Engine.Engine Engine = null) : base(a => (long)a, Engine)
        {

        }
    }

    [Chip("SingleToDouble")]
    public sealed class ToDouble : UnaryFunctor<float, double>
    {
        public ToDouble(Engine.Engine Engine = null) : base(a => a, Engine)
        {

        }
    }

    [Chip("SingleToString")]
    public sealed class ToString : UnaryFunctor<float, string>
    {
        public ToString(Engine.Engine Engine = null) : base(a => a.ToString(), Engine)
        {

        }
    }
}
