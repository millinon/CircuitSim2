using ED = CircuitSim2.Chips.Signals.EdgeDetector<long>;

namespace CircuitSim2.Chips.Long.Signals
{
    [PureChip("LongRisingEdge")]
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base((a, b) => (a < b), Engine)
        {

        }
    }

    [PureChip("LongFallingEdge")]
    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base((a, b) => (a > b), Engine)
        {

        }
    }
}
