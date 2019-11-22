using CircuitSim2.Chips.Functors;
using R = CircuitSim2.Chips.Functors.Random<double>;

namespace CircuitSim2.Chips.Double.Generators
{
    [Chip("DoubleRandom")]
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

        protected sealed override double NextValue() => RNG.NextDouble();
    }

    [Chip("DoubleConstant")]
    public sealed class Constant : Constant<double>
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