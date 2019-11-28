using System;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<long, long, bool>;

namespace CircuitSim2.Chips.Long.Comparison
{
    [Chip("LongEqual")]
    [Serializable]
    public sealed class Equal : BF
    {
        public override bool Func(long Val1, long Val2) => Val1 == Val2;
    }

    [Chip("LongNotEqual")]
    [Serializable]
    public sealed class NotEqual : BF
    {
        public override bool Func(long Val1, long Val2) => Val1 != Val2;
    }

    [Chip("LongLessThan")]
    [Serializable]
    public sealed class LessThan : BF
    {
        public override bool Func(long Val1, long Val2) => Val1 < Val2;
    }

    [Chip("LongLessThanEqual")]
    [Serializable]
    public sealed class LessThanEqual : BF
    {
        public override bool Func(long Val1, long Val2) => Val1 <= Val2;
    }

    [Chip("LongGreaterThan")]
    [Serializable]
    public sealed class GreaterThan : BF
    {
        public override bool Func(long Val1, long Val2) => Val1 > Val2;
    }

    [Chip("LongGreaterThanEqual")]
    [Serializable]
    public sealed class GreaterThanEqual : BF
    {
        public override bool Func(long Val1, long Val2) => Val1 >= Val2;
    }
}
