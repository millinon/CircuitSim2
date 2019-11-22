using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Char.Generators
{
    [Chip("CharConstant")]
    public sealed class Constant : Constant<char>
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
