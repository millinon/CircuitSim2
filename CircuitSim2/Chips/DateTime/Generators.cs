using System;
using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.DateTime.Generators
{
    [Chip("DateTimeConstant")]
    [Serializable]
    public sealed class Constant : Constant<System.DateTime>
    {
    }

    [Chip("DateTimeNow")]
    [Serializable]
    public sealed class Now : Generator<System.DateTime>
    {
        protected override System.DateTime NextValue() => System.DateTime.Now;
    }
}