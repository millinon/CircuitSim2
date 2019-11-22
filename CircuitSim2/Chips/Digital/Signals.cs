using ED = CircuitSim2.Chips.Signals.EdgeDetector<bool>;

namespace CircuitSim2.Chips.Digital.Signals
{
    [Chip("DigitalRisingEdge")]
    public class RisingEdge : ED
    {
        public RisingEdge(ChipBase ParentChip, Engine.Engine Engine) : base((a, b) => !a && b, ParentChip, Engine)
        {
        }

        public RisingEdge(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public RisingEdge(Engine.Engine Engine) : this(null, Engine)
        {
        }
    }

    [Chip("DigitalFallingEdge")]
    public sealed class FallingEdge : ED
    {
        public FallingEdge(ChipBase ParentChip, Engine.Engine Engine) : base((a, b) => a && !b, ParentChip, Engine)
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
