using ED = CircuitSim2.Chips.Signals.EdgeDetector<long>;

namespace CircuitSim2.Chips.Long.Signals
{
    [Chip("LongRisingEdge")]
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

    [Chip("LongFallingEdge")]
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
