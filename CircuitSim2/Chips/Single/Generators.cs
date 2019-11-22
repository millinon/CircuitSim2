using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Single.Generators
{
    [Chip("SingleConstant")]
    public sealed class Constant : Constant<float>
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
