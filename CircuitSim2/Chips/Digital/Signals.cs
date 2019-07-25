using ED = CircuitSim2.Chips.Signals.EdgeDetector<bool>;

namespace CircuitSim2.Chips.Digital.Signals
{
    [Chip("DigitalRisingEdge")]
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base((a, b) => (!a && b), Engine)
        {

        }
    }

    [Chip("DigitalFallingEdge")]
    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base((a, b) => (a && !b), Engine)
        {

        }
    }
}
