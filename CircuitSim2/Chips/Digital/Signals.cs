using ED = CircuitSim2.Chips.Signals.EdgeDetector<bool>;

namespace CircuitSim2.Chips.Digital.Signals
{
    [PureChip("DigitalRisingEdge")]
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base((a, b) => (!a && b), Engine)
        {

        }
    }

    [PureChip("DigitalFallingEdge")]
    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base((a, b) => (a && !b), Engine)
        {

        }
    }
}
