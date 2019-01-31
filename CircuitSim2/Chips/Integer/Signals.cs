using ED = CircuitSim2.Chips.Signals.EdgeDetector<int>;

namespace CircuitSim2.Chips.Integer.Signals
{
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base("IntegerRisingEdge", (a, b) => (a < b), Engine)
        {

        }
    }

    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base("IntegerFallingEdge", (a, b) => (a > b), Engine)
        {

        }
    }
}
