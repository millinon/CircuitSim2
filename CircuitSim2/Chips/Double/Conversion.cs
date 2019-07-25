using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Double.Conversion
{
    [Chip("DoubleToByte")]
    public sealed class ToByte : UnaryFunctor<double, byte>
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

        public override byte Func(double Value) => (byte)Value;
    }

    [Chip("DoubleToChar")]
    public sealed class ToChar : UnaryFunctor<double, char>
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

        public override char Func(double Value) => (char)Value;
    }

    [Chip("DoubleToLong")]
    public sealed class ToLong : UnaryFunctor<double, long>
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

        public override long Func(double Value) => (long)Value;
    }

    [Chip("DoubleToSingle")]
    public sealed class ToSingle : UnaryFunctor<double, float>
    {
        private ToSingle(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToSingle(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToSingle(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToSingle() : this(null, null)
        {
        }

        public override float Func(double Value) => (float)Value;
    }

    [Chip("DoubleToInteger")]
    public sealed class ToInteger : UnaryFunctor<double, int>
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

        public override int Func(double Value) => (int)Value;
    }

    [Chip("DoubleToString")]
    public sealed class ToString : UnaryFunctor<double, string>
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

        public override string Func(double Value) => Value.ToString();
    }

    [Chip("DoubleToHexString")]
    public sealed class ToHexString : UnaryFunctor<double, string>
    {
        private ToHexString(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToHexString(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToHexString(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToHexString() : this(null, null)
        {
        }

        public override string Func(double Value) => Value.ToString("X");
    }
}
