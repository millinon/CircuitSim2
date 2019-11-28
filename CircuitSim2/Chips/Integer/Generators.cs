using CircuitSim2.Chips.Functors;
using System;
using R = CircuitSim2.Chips.Functors.Random<int>;

namespace CircuitSim2.Chips.Integer.Generators
{
    [Chip("IntegerRandom")]
    [Serializable]
    public sealed class Random : R
    {
        protected sealed override int NextValue() => RNG.Next();
    }

    [Chip("IntegerConstant")]
    [Serializable]
    public sealed class Constant : Constant<int>
    {
    }
}
