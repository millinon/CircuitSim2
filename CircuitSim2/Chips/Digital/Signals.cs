using System;
using ED = CircuitSim2.Chips.Signals.EdgeDetector<bool>;

namespace CircuitSim2.Chips.Digital.Signals
{
    [Chip("DigitalRisingEdge")]
    [Serializable]
    public class RisingEdge : ED
    {
        protected override bool Detector(bool A, bool B) => !A && B;
    }

    [Chip("DigitalFallingEdge")]
    [Serializable]
    public sealed class FallingEdge : ED
    {
        protected override bool Detector(bool A, bool B) => A && !B;
    }
}
