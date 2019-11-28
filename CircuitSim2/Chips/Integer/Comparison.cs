using System;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<int, int, bool>;

namespace CircuitSim2.Chips.Integer.Comparison
{
    [Chip("IntegerEqual")]
    [Serializable]
    public sealed class Equal : BF
    {
        public override bool Func(int Val1, int Val2) => Val1 == Val2;
    }

    [Chip("IntegerNotEqual")]
    [Serializable]
    public sealed class NotEqual : BF
    {
        public override bool Func(int Val1, int Val2) => Val1 != Val2;
    }

    [Chip("IntegerLessThan")]
    [Serializable]
    public sealed class LessThan : BF
    {
        public override bool Func(int Val1, int Val2) => Val1 < Val2;
    }

    [Chip("IntegerLessThanEqual")]
    [Serializable]
    public sealed class LessThanEqual : BF
    {
        public override bool Func(int Val1, int Val2) => Val1 <= Val2;
    }

    [Chip("IntegerGreaterThan")]
    [Serializable]
    public sealed class GreaterThan : BF
    {
        public override bool Func(int Val1, int Val2) => Val1 > Val2;
    }

    [Chip("IntegerGreaterThanEqual")]
    [Serializable]
    public sealed class GreaterThanEqual : BF
    {
        public override bool Func(int Val1, int Val2) => Val1 >= Val2;
    }
}
