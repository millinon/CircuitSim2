using ED = CircuitSim2.Chips.Signals.EdgeDetector<bool>;

namespace CircuitSim2.Chips.Digital.Signals
{
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base("DigitalRisingEdge", (a, b) => (!a && b), Engine)
        {

        }
    }

    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base("DigitalFallingEdge", (a, b) => (a && !b), Engine)
        {

        }
    }
}
