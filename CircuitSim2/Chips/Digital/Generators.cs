using CircuitSim2.Chips.Functors;
using System;
using R = CircuitSim2.Chips.Functors.Random<bool>;

namespace CircuitSim2.Chips.Digital.Generators
{
    [Chip("DigitalRandom")]
    [Serializable]
    public sealed class Random : R
    {
        protected override bool NextValue() => this.RNG.NextDouble() <= 0.5;
    }

    [Chip("DigitalConstant")]
    [Serializable]
    public sealed class Constant : Constant<bool>
    {
    }
}
