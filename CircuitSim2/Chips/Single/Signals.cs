using ED = CircuitSim2.Chips.Signals.EdgeDetector<float>;

namespace CircuitSim2.Chips.Single.Signals
{
    [Chip("SingleRisingEdge")]
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

    [Chip("SingleFallingEdge")]
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
