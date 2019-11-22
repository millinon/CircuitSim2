using ED = CircuitSim2.Chips.Signals.EdgeDetector<System.TimeSpan>;

namespace CircuitSim2.Chips.TimeSpan.Signals
{
    [Chip("TimeSpanRisingEdge")]
    public class RisingEdge : ED
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

    [Chip("TimeSpanFallingEdge")]
    public sealed class FallingEdge : ED
    {
        public FallingEdge(ChipBase ParentChip, Engine.Engine Engine) : base((a, b) => a > b, ParentChip, Engine)
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
