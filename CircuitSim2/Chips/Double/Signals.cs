using ED = CircuitSim2.Chips.Signals.EdgeDetector<double>;

namespace CircuitSim2.Chips.Double.Signals
{
    [PureChip("DoubleRisingEdge")]
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base((a, b) => (a < b), Engine)
        {

        }
    }

    [PureChip("DoubleFallingEdge")]
    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base((a, b) => (a > b), Engine)
        {

        }
    }
}
