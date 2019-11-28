using CircuitSim2.Chips.Functors;
using System;
using R = CircuitSim2.Chips.Functors.Random<long>;

namespace CircuitSim2.Chips.Long.Generators
{
    [Chip("LongRandom")]
    [Serializable]
    public sealed class Random : R
    {
        protected override long NextValue() => ((long)RNG.Next() << 32) + RNG.Next();
    }

    [Chip("LongConstant")]
    [Serializable]
    public sealed class Constant : Constant<long>
    {
    }
}
