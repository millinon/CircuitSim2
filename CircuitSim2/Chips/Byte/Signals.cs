﻿using ED = CircuitSim2.Chips.Signals.EdgeDetector<byte>;

namespace CircuitSim2.Chips.Byte.Signals
{
    public sealed class RisingEdge : ED
    {
        public RisingEdge(Engine.Engine Engine = null) : base("ByteRisingEdge", (a, b) => (a < b), Engine)
        {

        }
    }

    public sealed class FallingEdge : ED
    {
        public FallingEdge(Engine.Engine Engine = null) : base("ByteFallingEdge", (a, b) => (a > b), Engine)
        {

        }
    }
}