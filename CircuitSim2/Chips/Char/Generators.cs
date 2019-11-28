using CircuitSim2.Chips.Functors;
using System;

namespace CircuitSim2.Chips.Char.Generators
{
    [Chip("CharConstant")]
    [Serializable]
    public sealed class Constant : Constant<char>
    {
    }
}
