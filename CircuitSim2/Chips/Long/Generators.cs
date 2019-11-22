using CircuitSim2.Chips.Functors;
using R = CircuitSim2.Chips.Functors.Random<long>;

namespace CircuitSim2.Chips.Long.Generators
{
    [Chip("LongRandom")]
    public sealed class Random : R
    {
        private Random(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Random(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Random(Engine.Engine Engine) : this(null, Engine)
        {
        }

        protected override long NextValue() => ((long)RNG.Next() << 32) + RNG.Next();
    }

    [Chip("LongConstant")]
    public sealed class Constant : Constant<long>
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
