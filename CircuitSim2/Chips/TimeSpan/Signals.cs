using System;
using ED = CircuitSim2.Chips.Signals.EdgeDetector<System.TimeSpan>;

namespace CircuitSim2.Chips.TimeSpan.Signals
{
    [Chip("TimeSpanRisingEdge")]
    [Serializable]
    public class RisingEdge : ED
    {
        protected override bool Detector(System.TimeSpan A, System.TimeSpan B) => A < B;
    }

    [Chip("TimeSpanFallingEdge")]
    [Serializable]
    public sealed class FallingEdge : ED
    {
        protected override bool Detector(System.TimeSpan A, System.TimeSpan B) => A > B;
    }
}
