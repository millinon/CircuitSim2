using ED = CircuitSim2.Chips.Signals.EdgeDetector<byte>;

namespace CircuitSim2.Chips.Byte.Signals
{
    [PureChip("ByteRisingEdge")]
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base((a, b) => (a < b), Engine)
        {

        }
    }

    [PureChip("ByteFallingEdge")]
    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base((a, b) => (a > b), Engine)
        {

        }
    }
}
