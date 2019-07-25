using ED = CircuitSim2.Chips.Signals.EdgeDetector<double>;

namespace CircuitSim2.Chips.Double.Signals
{
    [Chip("DoubleRisingEdge")]
    public sealed class RisingEdge : ED
    {
        private RisingEdge(ChipBase ParentChip, Engine.Engine Engine) : base((a, b) => a < b, ParentChip, Engine)
        {
        }

        public RisingEdge(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public RisingEdge(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public RisingEdge() : this(null, null)
        {
        }
    }

    [Chip("DoubleFallingEdge")]
    public sealed class FallingEdge : ED
    {
        private FallingEdge(ChipBase ParentChip, Engine.Engine Engine) : base((a, b) => a < b, ParentChip, Engine)
        {
        }

        public FallingEdge(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public FallingEdge(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public FallingEdge() : this(null, null)
        {
        }
    }
}
