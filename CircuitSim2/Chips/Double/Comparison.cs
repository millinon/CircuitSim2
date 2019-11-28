using System;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<double, double, bool>;

namespace CircuitSim2.Chips.Double.Comparison
{
    [Chip("DoubleEqual")]
    [Serializable]
    public sealed class Equal : BF
    {
        public override bool Func(double Val1, double Val2) => Val1 == Val2;
    }

    [Chip("DoubleNotEqual")]
    [Serializable]
    public sealed class NotEqual : BF
    {
        public override bool Func(double Val1, double Val2) => Val1 != Val2;
    }

    [Chip("DoubleLessThan")]
    [Serializable]
    public sealed class LessThan : BF
    {
        public override bool Func(double Val1, double Val2) => Val1 < Val2;
    }

    [Chip("DoubleLessThanEqual")]
    [Serializable]
    public sealed class LessThanEqual : BF
    {
        public override bool Func(double Val1, double Val2) => Val1 <= Val2;
    }

    [Chip("DoubleGreaterThan")]
    [Serializable]
    public sealed class GreaterThan : BF
    {
        public override bool Func(double Val1, double Val2) => Val1 > Val2;
    }

    [Chip("DoubleGreaterThanEqual")]
    [Serializable]
    public sealed class GreaterThanEqual : BF
    {
        public override bool Func(double Val1, double Val2) => Val1 >= Val2;
    }
}
