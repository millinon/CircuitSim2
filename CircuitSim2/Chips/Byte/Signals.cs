using System;
using ED = CircuitSim2.Chips.Signals.EdgeDetector<byte>;

namespace CircuitSim2.Chips.Byte.Signals
{
    [Chip("ByteRisingEdge")]
    [Serializable]
    public sealed class RisingEdge : ED
    {
        protected override bool Detector(byte A, byte B) => A < B;
    }

    [Chip("ByteFallingEdge")]
    [Serializable]
    public sealed class FallingEdge : ED
    {
        protected override bool Detector(byte A, byte B) => A > B;
    }

    [Chip("ByteLag")]
    [Serializable]
    public sealed class Lag : Chips.Signals.Lag<byte>
    {
    }
}
