using BF = CircuitSim2.Chips.Functors.BinaryFunctor<int, int, bool>;

namespace CircuitSim2.Chips.Integer.Comparison
{
    [Chip("IntegerEqual")]
    public sealed class Equal : BF
    {
        private Equal(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Equal(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Equal(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public Equal() : this(null, null)
        {
        }

        public override bool Func(int Val1, int Val2) => Val1 == Val2;
    }

    [Chip("IntegerNotEqual")]
    public sealed class NotEqual : BF
    {
        private NotEqual(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public NotEqual(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public NotEqual(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public NotEqual() : this(null, null)
        {
        }

        public override bool Func(int Val1, int Val2) => Val1 != Val2;
    }

    [Chip("IntegerLessThan")]
    public sealed class LessThan : BF
    {
        private LessThan(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public LessThan(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public LessThan(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public LessThan() : this(null, null)
        {
        }

        public override bool Func(int Val1, int Val2) => Val1 < Val2;
    }

    [Chip("IntegerLessThanEqual")]
    public sealed class LessThanEqual : BF
    {
        private LessThanEqual(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public LessThanEqual(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public LessThanEqual(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public LessThanEqual() : this(null, null)
        {
        }

        public override bool Func(int Val1, int Val2) => Val1 <= Val2;
    }

    [Chip("IntegerGreaterThan")]
    public sealed class GreaterThan : BF
    {
        private GreaterThan(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public GreaterThan(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public GreaterThan(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public GreaterThan() : this(null, null)
        {
        }

        public override bool Func(int Val1, int Val2) => Val1 > Val2;
    }

    [Chip("IntegerGreaterThanEqual")]
    public sealed class GreaterThanEqual : BF
    {
        private GreaterThanEqual(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public GreaterThanEqual(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public GreaterThanEqual(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public GreaterThanEqual() : this(null, null)
        {
        }

        public override bool Func(int Val1, int Val2) => Val1 >= Val2;
    }
}
