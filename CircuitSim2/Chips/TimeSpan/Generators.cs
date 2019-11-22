using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.TimeSpan.Generators
{
    [Chip("TimeSpanConstant")]
    public sealed class Constant : Constant<System.TimeSpan>
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