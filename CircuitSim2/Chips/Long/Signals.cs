using ED = CircuitSim2.Chips.Signals.EdgeDetector<long>;

namespace CircuitSim2.Chips.Long.Signals
{
    [Chip("LongRisingEdge")]
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base((a, b) => (a < b), Engine)
        {

        }
    }

    [Chip("LongFallingEdge")]
    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base((a, b) => (a > b), Engine)
        {

        }
    }
}
