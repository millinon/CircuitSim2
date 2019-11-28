using CircuitSim2.Chips.Functors;
using System;

namespace CircuitSim2.Chips.String.Generators
{
    [Chip("StringConstant")]
    [Serializable]
    public sealed class Constant : Constant<string>
    {
    }
}
