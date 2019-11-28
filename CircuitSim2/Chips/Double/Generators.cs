using CircuitSim2.Chips.Functors;
using System;
using R = CircuitSim2.Chips.Functors.Random<double>;

namespace CircuitSim2.Chips.Double.Generators
{
    [Chip("DoubleRandom")]
    [Serializable]
    public sealed class Random : R
    {
        protected sealed override double NextValue() => RNG.NextDouble();
    }

    [Chip("DoubleConstant")]
    [Serializable]
    public sealed class Constant : Constant<double>
    {
    }
}