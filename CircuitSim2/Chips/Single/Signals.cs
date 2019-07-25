using ED = CircuitSim2.Chips.Signals.EdgeDetector<float>;

namespace CircuitSim2.Chips.Single.Signals
{
    [Chip("SingleRisingEdge")]
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base((a, b) => (a < b), Engine)
        {

        }
    }

    [Chip("SingleFallingEdge")]
    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base((a, b) => (a > b), Engine)
        {

        }
    }
}
