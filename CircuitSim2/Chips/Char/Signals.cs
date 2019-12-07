using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitSim2.Chips.Char.Signals
{
    [Chip("CharLag")]
    [Serializable]
    public sealed class Lag : Chips.Signals.Lag<char>
    {
    }
}
