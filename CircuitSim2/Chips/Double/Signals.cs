using ED = CircuitSim2.Chips.Signals.EdgeDetector<double>;

namespace CircuitSim2.Chips.Double.Signals
{
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base("DoubleRisingEdge", (a, b) => (a < b), Engine)
        {

        }
    }

    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base("DoubleFallingEdge", (a, b) => (a > b), Engine)
        {

        }
    }
}
