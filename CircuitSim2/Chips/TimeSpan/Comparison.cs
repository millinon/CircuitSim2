using System;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<System.TimeSpan, System.TimeSpan, bool>;

namespace CircuitSim2.Chips.TimeSpan.Comparison
{
    [Chip("TimeSpanEqual")]
    [Serializable]
    public sealed class Equal : BF
    {
        public override bool Func(System.TimeSpan Value1, System.TimeSpan Value2) => Value1 == Value2;
    }

    [Chip("TimeSpanNotEqual")]
    [Serializable]
    public sealed class NotEqual : BF
    {
        public override bool Func(System.TimeSpan Val1, System.TimeSpan Val2) => Val1 != Val2;
    }

    [Chip("TimeSpanLessThan")]
    [Serializable]
    public sealed class LessThan : BF
    {
        public override bool Func(System.TimeSpan Val1, System.TimeSpan Val2) => System.TimeSpan.Compare(Val1, Val2) < 0;
    }

    [Chip("TimeSpanLessThanEqual")]
    [Serializable]
    public sealed class LessThanEqual : BF
    {
        public override bool Func(System.TimeSpan Val1, System.TimeSpan Val2) => System.TimeSpan.Compare(Val1, Val2) <= 0;
    }

    [Chip("TimeSpanGreaterThan")]
    [Serializable]
    public sealed class GreaterThan : BF
    {
        public override bool Func(System.TimeSpan Val1, System.TimeSpan Val2) => System.TimeSpan.Compare(Val1, Val2) > 0;
    }

    [Chip("TimeSpanGreaterThanEqual")]
    [Serializable]
    public sealed class GreaterThanEqual : BF
    {
        public override bool Func(System.TimeSpan Val1, System.TimeSpan Val2) => System.TimeSpan.Compare(Val1, Val2) >= 0;
    }

    [Chip("TimeSpanCompare")]
    [Serializable]
    public sealed class Compare : Chips.Functors.BinaryFunctor<System.TimeSpan, System.TimeSpan, int>
    {
        public override int Func(System.TimeSpan Val1, System.TimeSpan Val2) => System.TimeSpan.Compare(Val1, Val2);
    }
}
