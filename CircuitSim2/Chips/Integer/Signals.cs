using ED = CircuitSim2.Chips.Signals.EdgeDetector<int>;

namespace CircuitSim2.Chips.Integer.Signals
{
    [Chip("IntegerRisingEdge")]
    public sealed class RisingEdge : ED
    {
        public RisingEdge(ChipBase ParentChip, Engine.Engine Engine) : base((a, b) => a < b, ParentChip, Engine)
        {
        }

        public RisingEdge(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public RisingEdge(Engine.Engine Engine) : this(null, Engine)
        {
        }
    }

    [Chip("IntegerFallingEdge")]
    public sealed class FallingEdge : ED
    {
        public FallingEdge(ChipBase ParentChip, Engine.Engine Engine) : base((a, b) => a < b, ParentChip, Engine)
        {
        }

        public FallingEdge(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public FallingEdge(Engine.Engine Engine) : this(null, Engine)
        {
        }
    }
}
