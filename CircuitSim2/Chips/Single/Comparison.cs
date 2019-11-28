using System;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<float, float, bool>;

namespace CircuitSim2.Chips.Single.Comparison
{
    [Chip("SingleEqual")]
    [Serializable]
    public sealed class Equal : BF
    {
        public override bool Func(float Val1, float Val2) => Val1 == Val2;
    }

    [Chip("SingleNotEqual")]
    [Serializable]
    public sealed class NotEqual : BF
    {

        public override bool Func(float Val1, float Val2) => Val1 != Val2;
    }

    [Chip("SingleLessThan")]
    [Serializable]
    public sealed class LessThan : BF
    {
        public override bool Func(float Val1, float Val2) => Val1 < Val2;
    }

    [Chip("SingleLessThanEqual")]
    [Serializable]
    public sealed class LessThanEqual : BF
    {

        public override bool Func(float Val1, float Val2) => Val1 <= Val2;
    }

    [Chip("SingleGreaterThan")]
    [Serializable]
    public sealed class GreaterThan : BF
    {
        public override bool Func(float Val1, float Val2) => Val1 > Val2;
    }

    [Chip("SingleGreaterThanEqual")]
    [Serializable]
    public sealed class GreaterThanEqual : BF
    {
        public override bool Func(float Val1, float Val2) => Val1 >= Val2;
    }
}
