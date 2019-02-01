using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Integer.Conversion
{
    [PureChip("IntegerToByte")]
    public sealed class ToByte : UnaryFunctor<int, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base(a => (byte)a, Engine)
        {

        }
    }

    [PureChip("IntegerToChar")]
    public sealed class ToChar : UnaryFunctor<int, char>
    {
        public ToChar(Engine.Engine Engine = null) : base(a => (char)a, Engine)
        {

        }
    }

    [PureChip("IntegerToLong")]
    public sealed class ToLong : UnaryFunctor<int, long>
    {
        public ToLong(Engine.Engine Engine = null) : base(a => a, Engine)
        {

        }
    }

    [PureChip("IntegerToSingle")]
    public sealed class ToSingle : UnaryFunctor<int, float>
    {
        public ToSingle(Engine.Engine Engine = null) : base(a => a, Engine)
        {

        }
    }

    [PureChip("IntegerToDouble")]
    public sealed class ToDouble : UnaryFunctor<int, double>
    {
        public ToDouble(Engine.Engine Engine = null) : base(a => a, Engine)
        {

        }
    }

    [PureChip("IntegerToString")]
    public sealed class ToString : UnaryFunctor<int, string>
    {
        public ToString(Engine.Engine Engine = null) : base(a => a.ToString(), Engine)
        {

        }
    }
}
