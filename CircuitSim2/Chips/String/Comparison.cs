using System;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<string, string, bool>;

namespace CircuitSim2.Chips.String.Comparison
{
    [Chip("StringEqual")]
    [Serializable]
    public sealed class Equal : BF
    {
        public override bool Func(string Value1, string Value2) => Value1 == Value2;
    }

    [Chip("StringNotEqual")]
    [Serializable]
    public sealed class NotEqual : BF
    {
        public override bool Func(string Val1, string Val2) => Val1 != Val2;
    }

    [Chip("StringLessThan")]
    [Serializable]
    public sealed class LessThan : BF
    {
        public override bool Func(string Val1, string Val2) => string.Compare(Val1, Val2) < 0;
    }

    [Chip("StringLessThanEqual")]
    [Serializable]
    public sealed class LessThanEqual : BF
    {
        public override bool Func(string Val1, string Val2) => string.Compare(Val1, Val2) <= 0;
    }

    [Chip("StringGreaterThan")]
    [Serializable]
    public sealed class GreaterThan : BF
    {
        public override bool Func(string Val1, string Val2) => string.Compare(Val1, Val2) > 0;
    }

    [Chip("StringGreaterThanEqual")]
    [Serializable]
    public sealed class GreaterThanEqual : BF
    {
        public override bool Func(string Val1, string Val2) => string.Compare(Val1, Val2) >= 0;
    }

    [Chip("StringCompare")]
    [Serializable]
    public sealed class Compare : Chips.Functors.BinaryFunctor<string, string, int>
    {
        public override int Func(string Val1, string Val2) => string.Compare(Val1, Val2);
    }
}
