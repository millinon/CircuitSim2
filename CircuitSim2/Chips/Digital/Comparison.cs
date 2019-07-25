using BF = CircuitSim2.Chips.Functors.BinaryFunctor<bool, bool, bool>;

namespace CircuitSim2.Chips.Digital.Comparison
{
    [Chip("DigitalEqual")]
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

        public override bool Func(bool Val1, bool Val2) => Val1 == Val2;
    }

    [Chip("DigitalNotEqual")]
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

        public override bool Func(bool Val1, bool Val2) => Val1 != Val2;
    }
}
