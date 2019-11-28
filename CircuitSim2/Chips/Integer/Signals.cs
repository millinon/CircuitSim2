using System;
using ED = CircuitSim2.Chips.Signals.EdgeDetector<int>;

namespace CircuitSim2.Chips.Integer.Signals
{
    [Chip("IntegerRisingEdge")]
    [Serializable]
    public sealed class RisingEdge : ED
    {
        protected override bool Detector(int A, int B) => A < B;
    }

    [Chip("IntegerFallingEdge")]
    [Serializable]
    public sealed class FallingEdge : ED
    {
        protected override bool Detector(int A, int B) => A > B;
    }
}
