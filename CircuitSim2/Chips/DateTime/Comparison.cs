using System;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<System.DateTime, System.DateTime, bool>;

namespace CircuitSim2.Chips.DateTime.Comparison
{
    [Chip("DateTimeEqual")]
    [Serializable]
    public sealed class Equal : BF
    {
        public override bool Func(System.DateTime Value1, System.DateTime Value2) => Value1 == Value2;
    }

    [Chip("DateTimeNotEqual")]
    [Serializable]
    public sealed class NotEqual : BF
    {
        public override bool Func(System.DateTime Val1, System.DateTime Val2) => Val1 != Val2;
    }

    [Chip("DateTimeLessThan")]
    [Serializable]
    public sealed class LessThan : BF
    {
        public override bool Func(System.DateTime Val1, System.DateTime Val2) => System.DateTime.Compare(Val1, Val2) < 0;
    }

    [Chip("DateTimeLessThanEqual")]
    [Serializable]
    public sealed class LessThanEqual : BF
    {
        public override bool Func(System.DateTime Val1, System.DateTime Val2) => System.DateTime.Compare(Val1, Val2) <= 0;
    }

    [Chip("DateTimeGreaterThan")]
    [Serializable]
    public sealed class GreaterThan : BF
    {
        public override bool Func(System.DateTime Val1, System.DateTime Val2) => System.DateTime.Compare(Val1, Val2) > 0;
    }

    [Chip("DateTimeGreaterThanEqual")]
    [Serializable]
    public sealed class GreaterThanEqual : BF
    {
        public override bool Func(System.DateTime Val1, System.DateTime Val2) => System.DateTime.Compare(Val1, Val2) >= 0;
    }

    [Chip("DateTimeCompare")]
    [Serializable]
    public sealed class Compare : Chips.Functors.BinaryFunctor<System.DateTime, System.DateTime, int>
    {
        public override int Func(System.DateTime Val1, System.DateTime Val2) => System.DateTime.Compare(Val1, Val2);
    }
}
