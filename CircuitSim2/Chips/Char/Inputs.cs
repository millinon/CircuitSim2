using System;

namespace CircuitSim2.Chips.Char.Inputs
{
    [Chip("CharSwitch")]
    [Serializable]
    public sealed class Switch : IO.Inputs.Switch<char>
    {
    }
}
