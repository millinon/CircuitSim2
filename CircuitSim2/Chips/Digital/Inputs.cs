using System;

namespace CircuitSim2.Chips.Digital.Inputs
{
    [Chip("DigitalSwitch")]
    [Serializable]
    public sealed class Switch : IO.Inputs.Switch<bool>
    {
    }
}
