using System;

namespace CircuitSim2.Chips.String.Signals
{
    [Chip("StringLag")]
    [Serializable]
    public sealed class Lag : Chips.Signals.Lag<string>
    {
    }
}
