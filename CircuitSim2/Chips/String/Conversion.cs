using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.String.Conversion
{
    [Chip("StringToByte")]
    public sealed class ToByte : UnaryFunctor<string, byte>
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

        public override byte Func(string Value) => byte.Parse(Value);
    }

    [Chip("StringToInteger")]
    public sealed class ToInteger : UnaryFunctor<string, int>
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

        public override int Func(string Value) => int.Parse(Value);
    }

    [Chip("StringToSingle")]
    public sealed class ToSingle : UnaryFunctor<string, float>
    {
        public ToSingle(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToSingle(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToSingle(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override float Func(string Value) => float.Parse(Value);
    }

    [Chip("StringToDouble")]
    public sealed class ToDouble : UnaryFunctor<string, double>
    {
        public ToDouble(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToDouble(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToDouble(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(string Value) => double.Parse(Value);
    }

    [Chip("StringToDateTime")]
    public sealed class ToDateTime : UnaryFunctor<string, System.DateTime>
    {
        public ToDateTime(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToDateTime(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToDateTime(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override System.DateTime Func(string Value) => System.DateTime.Parse(Value);
    }

}
