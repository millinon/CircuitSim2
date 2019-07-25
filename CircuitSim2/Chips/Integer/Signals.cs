using ED = CircuitSim2.Chips.Signals.EdgeDetector<int>;

namespace CircuitSim2.Chips.Integer.Signals
{
    [Chip("IntegerRisingEdge")]
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base((a, b) => (a < b), Engine)
        {

        }
    }

    [Chip("IntegerFallingEdge")]
    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base((a, b) => (a > b), Engine)
        {

        }
    }
}
