using CircuitSim2.Chips.Functors;
using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<long, long, long>;

namespace CircuitSim2.Chips.Long.Arithmetic
{
    [Chip("LongAdd")]
    [Serializable]
    public sealed class Add : BF
    {
        public override long Func(long Val1, long Val2) => Val1 + Val2;
    }

    [Chip("LongSubtract")]
    [Serializable]
    public sealed class Subtract : BF
    {
        public override long Func(long Val1, long Val2) => Val1 - Val2;
    }

    [Chip("LongMultiply")]
    [Serializable]
    public sealed class Multiply : BF
    {
        public override long Func(long Val1, long Val2) => Val1 * Val2;
    }

    [Chip("LongDivide")]
    [Serializable]
    public sealed class Divide : BF
    {
        public override long Func(long Val1, long Val2) => Val1 / Val2;
    }

    [Chip("LongModulus")]
    [Serializable]
    public sealed class Modulus : BF
    {
        public override long Func(long Val1, long Val2) => Val1 % Val2;
    }

    [Chip("LongMin")]
    [Serializable]
    public sealed class Min : BF
    {
        public override long Func(long Val1, long Val2) => Math.Min(Val1, Val2);
    }

    [Chip("LongMax")]
    [Serializable]
    public sealed class Max : BF
    {
        public override long Func(long Val1, long Val2) => Math.Max(Val1, Val2);
    }

    [Chip("LongClamp")]
    [Serializable]
    public sealed class Clamp : Clamp<long>
    {
    }
}
