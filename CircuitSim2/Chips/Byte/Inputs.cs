using System;

namespace CircuitSim2.Chips.Byte.Inputs
{
    [Chip("ByteSwitch")]
    [Serializable]
    public sealed class Switch : IO.Inputs.Switch<byte>
    {
    }
}
