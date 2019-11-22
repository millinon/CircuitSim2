using CircuitSim2.Chips.Functors;
using R = CircuitSim2.Chips.Functors.Random<int>;

namespace CircuitSim2.Chips.Integer.Generators
{
    [Chip("IntegerRandom")]
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

        protected sealed override int NextValue() => RNG.Next();
    }

    [Chip("IntegerConstant")]
    public sealed class Constant : Constant<int>
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
