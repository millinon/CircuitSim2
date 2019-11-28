using CircuitSim2.Chips.Functors;
using System;

namespace CircuitSim2.Chips.TimeSpan.Generators
{
    [Chip("TimeSpanConstant")]
    [Serializable]
    public sealed class Constant : Constant<System.TimeSpan>
    {
    }
}