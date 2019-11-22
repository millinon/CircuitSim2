using BF = CircuitSim2.Chips.Functors.BinaryFunctor<byte, byte, bool>;

namespace CircuitSim2.Chips.Byte.Comparison
{
    [Chip("ByteEqual")]
    public sealed class Equal : BF
    {
        public Equal(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Equal(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Equal(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override bool Func(byte Val1, byte Val2) => Val1 == Val2;
    }

    [Chip("ByteNotEqual")]
    public sealed class NotEqual : BF
    {
        public NotEqual(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public NotEqual(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public NotEqual(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override bool Func(byte Val1, byte Val2) => Val1 != Val2;
    }

    [Chip("ByteLessThan")]
    public sealed class LessThan : BF
    {
        public LessThan(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public LessThan(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public LessThan(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override bool Func(byte Val1, byte Val2) => Val1 < Val2;
    }

    [Chip("ByteLessThanEqual")]
    public sealed class LessThanEqual : BF
    {
        public LessThanEqual(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public LessThanEqual(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public LessThanEqual(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override bool Func(byte Val1, byte Val2) => Val1 <= Val2;
    }

    [Chip("ByteGreaterThan")]
    public sealed class GreaterThan : BF
    {
        public GreaterThan(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public GreaterThan(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public GreaterThan(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override bool Func(byte Val1, byte Val2) => Val1 > Val2;
    }

    [Chip("ByteGreaterThanEqual")]
    public sealed class GreaterThanEqual : BF
    {
        public GreaterThanEqual(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public GreaterThanEqual(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public GreaterThanEqual(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override bool Func(byte Val1, byte Val2) => Val1 >= Val2;
    }
}
