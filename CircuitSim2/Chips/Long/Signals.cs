using ED = CircuitSim2.Chips.Signals.EdgeDetector<long>;

namespace CircuitSim2.Chips.Long.Signals
{
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base("LongRisingEdge", (a, b) => (a < b), Engine)
        {

        }
    }

    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base("LongFallingEdge", (a, b) => (a > b), Engine)
        {

        }
    }
}
