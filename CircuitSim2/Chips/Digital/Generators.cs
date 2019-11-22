using CircuitSim2.Chips.Functors;
using R = CircuitSim2.Chips.Functors.Random<bool>;

namespace CircuitSim2.Chips.Digital.Generators
{
    [Chip("DigitalRandom")]
    public sealed class Random : R
    {
        public Random(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Random(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Random(Engine.Engine Engine) : this(null, Engine)
        {
        }

        protected override bool NextValue() => this.RNG.NextDouble() <= 0.5;
    }

    [Chip("DigitalConstant")]
    public sealed class Constant : Constant<bool>
    {
        public Constant(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Constant(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Constant(Engine.Engine Engine) : this(null, Engine)
        {
        }
    }
}
