using System;
using ED = CircuitSim2.Chips.Signals.EdgeDetector<System.DateTime>;

namespace CircuitSim2.Chips.DateTime.Signals
{
    [Chip("DateTimeRisingEdge")]
    [Serializable]
    public class RisingEdge : ED
    {
        protected override bool Detector(System.DateTime A, System.DateTime B) => A < B;
    }

    [Chip("DateTimeFallingEdge")]
    [Serializable]
    public sealed class FallingEdge : ED
    {
        protected override bool Detector(System.DateTime A, System.DateTime B) => A > B;
    }

    [Chip("DatetimeLag")]
    [Serializable]
    public sealed class Lag : Chips.Signals.Lag<System.DateTime>
    {
    }
}
