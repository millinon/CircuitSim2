using BF = CircuitSim2.Chips.Functors.BinaryFunctor<double, double, bool>;

namespace CircuitSim2.Chips.Double.Comparison
{
    [Chip("DoubleEqual")]
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

        public override bool Func(double Val1, double Val2) => Val1 == Val2;
    }

    [Chip("DoubleNotEqual")]
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

        public override bool Func(double Val1, double Val2) => Val1 != Val2;
    }

    [Chip("DoubleLessThan")]
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

        public override bool Func(double Val1, double Val2) => Val1 < Val2;
    }

    [Chip("DoubleLessThanEqual")]
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

        public override bool Func(double Val1, double Val2) => Val1 <= Val2;
    }

    [Chip("DoubleGreaterThan")]
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

        public override bool Func(double Val1, double Val2) => Val1 > Val2;
    }

    [Chip("DoubleGreaterThanEqual")]
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

        public override bool Func(double Val1, double Val2) => Val1 >= Val2;
    }
}
