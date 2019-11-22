using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Char.Conversion
{
    [Chip("CharToByte")]
    public sealed class ToByte : UnaryFunctor<char, byte>
    {
        public ToByte(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToByte(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToByte(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override byte Func(char Value) => (byte)Value;
    }

    [Chip("CharToInteger")]
    public sealed class ToInteger : UnaryFunctor<char, int>
    {
        public ToInteger(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToInteger(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToInteger(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override int Func(char Value) => (int)Value;
    }

    [Chip("CharToLong")]
    public sealed class ToLong : UnaryFunctor<char, long>
    {
        public ToLong(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToLong(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToLong(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override long Func(char Value) => Value;
    }

    [Chip("CharToString")]
    public sealed class ToString : UnaryFunctor<char, string>
    {
        public ToString(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToString(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToString(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override string Func(char Value) => Value.ToString();
    }
}
