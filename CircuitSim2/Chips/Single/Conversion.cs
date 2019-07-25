using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Single.Conversion
{
    [Chip("SingleToByte")]
    public sealed class ToByte : UnaryFunctor<float, byte>
    {
        private ToByte(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToByte(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToByte(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToByte() : this(null, null)
        {
        }

        public override byte Func(float Value) => (byte)Value;
    }

    [Chip("SingleToChar")]
    public sealed class ToChar : UnaryFunctor<float, char>
    {
        private ToChar(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToChar(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToChar(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToChar() : this(null, null)
        {
        }

        public override char Func(float Value) => (char)Value;
    }

    [Chip("SingleToInteger")]
    public sealed class ToInteger : UnaryFunctor<float, int>
    {
        private ToInteger(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToInteger(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToInteger(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToInteger() : this(null, null)
        {
        }

        public override int Func(float Value) => (int)Value;
    }

    [Chip("SingleToLong")]
    public sealed class ToLong : UnaryFunctor<float, long>
    {
        private ToLong(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToLong(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToLong(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToLong() : this(null, null)
        {
        }
        public override long Func(float Value) => (long)Value;
    }

    [Chip("SingleToDouble")]
    public sealed class ToDouble : UnaryFunctor<float, double>
    {
        private ToDouble(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToDouble(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToDouble(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToDouble() : this(null, null)
        {
        }

        public override double Func(float Value) => Value;
    }

    [Chip("SingleToString")]
    public sealed class ToString : UnaryFunctor<float, string>
    {
        private ToString(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToString(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToString(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToString() : this(null, null)
        {
        }

        public override string Func(float Value) => Value.ToString();
    }
}
