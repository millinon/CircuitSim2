using ED = CircuitSim2.Chips.Signals.EdgeDetector<float>;

namespace CircuitSim2.Chips.Single.Signals
{
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base("SingleRisingEdge", (a, b) => (a < b), Engine)
        {

        }
    }

    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base("SingleFallingEdge", (a, b) => (a > b), Engine)
        {

        }
    }
}
