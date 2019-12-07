using System;
using ED = CircuitSim2.Chips.Signals.EdgeDetector<double>;

namespace CircuitSim2.Chips.Double.Signals
{
    [Chip("DoubleRisingEdge")]
    [Serializable]
    public sealed class RisingEdge : ED
    {
        protected override bool Detector(double A, double B) => A < B;
    }

    [Chip("DoubleFallingEdge")]
    [Serializable]
    public sealed class FallingEdge : ED
    {
        protected override bool Detector(double A, double B) => A > B;
    }

    [Chip("DoubleLag")]
    [Serializable]
    public sealed class Lag : Chips.Signals.Lag<double>
    {
    }
}
