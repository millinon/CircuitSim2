using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.String.Conversion
{
    [Chip("StringToByte")]
    public sealed class ToByte : UnaryFunctor<string, byte>
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

        public override byte Func(string Value) => byte.Parse(Value);
    }

    [Chip("StringToInteger")]
    public sealed class ToInteger : UnaryFunctor<string, int>
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

        public override int Func(string Value) => int.Parse(Value);
    }

    [Chip("StringToSingle")]
    public sealed class ToSingle : UnaryFunctor<string, float>
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

        public override float Func(string Value) => float.Parse(Value);
    }

    [Chip("StringToDouble")]
    public sealed class ToDouble : UnaryFunctor<string, double>
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

        public override double Func(string Value) => double.Parse(Value);
    }
}
