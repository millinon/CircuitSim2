using System;

namespace CircuitSim2.Chips.DateTime.Inputs
{
    [Chip("DateTimeSwitch")]
    [Serializable]
    public sealed class Switch : IO.Inputs.Switch<System.DateTime>
    {
    }
}
