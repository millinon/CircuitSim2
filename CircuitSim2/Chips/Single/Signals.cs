using System;
using ED = CircuitSim2.Chips.Signals.EdgeDetector<float>;

namespace CircuitSim2.Chips.Single.Signals
{
    [Chip("SingleRisingEdge")]
    [Serializable]
    public sealed class RisingEdge : ED
    {
        protected override bool Detector(float A, float B) => A < B;
    }

    [Chip("SingleFallingEdge")]
    [Serializable]
    public sealed class FallingEdge : ED
    {
        protected override bool Detector(float A, float B) => A > B;
    }
}
