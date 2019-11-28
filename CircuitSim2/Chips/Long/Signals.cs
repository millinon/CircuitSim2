using System;
using ED = CircuitSim2.Chips.Signals.EdgeDetector<long>;

namespace CircuitSim2.Chips.Long.Signals
{
    [Chip("LongRisingEdge")]
    [Serializable]
    public sealed class RisingEdge : ED
    {
        protected override bool Detector(long A, long B)
        {
            return A < B;
        }
    }

    [Chip("LongFallingEdge")]
    [Serializable]
    public sealed class FallingEdge : ED
    {
        protected override bool Detector(long A, long B)
        {
            return A > B;
        }
    }
}
