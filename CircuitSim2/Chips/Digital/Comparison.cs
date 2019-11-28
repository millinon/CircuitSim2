using System;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<bool, bool, bool>;

namespace CircuitSim2.Chips.Digital.Comparison
{
    [Chip("DigitalEqual")]
    [Serializable]
    public sealed class Equal : BF
    {
        public override bool Func(bool Val1, bool Val2) => Val1 == Val2;
    }

    [Chip("DigitalNotEqual")]
    [Serializable]
    public sealed class NotEqual : BF
    {
        public override bool Func(bool Val1, bool Val2) => Val1 != Val2;
    }
}
