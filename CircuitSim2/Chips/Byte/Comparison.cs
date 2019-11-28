using System;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<byte, byte, bool>;

namespace CircuitSim2.Chips.Byte.Comparison
{
    [Chip("ByteEqual")]
    [Serializable]
    public sealed class Equal : BF
    {
        public override bool Func(byte Val1, byte Val2) => Val1 == Val2;
    }

    [Chip("ByteNotEqual")]
    [Serializable]
    public sealed class NotEqual : BF
    {
        public override bool Func(byte Val1, byte Val2) => Val1 != Val2;
    }

    [Chip("ByteLessThan")]
    [Serializable]
    public sealed class LessThan : BF
    {
        public override bool Func(byte Val1, byte Val2) => Val1 < Val2;
    }

    [Chip("ByteLessThanEqual")]
    [Serializable]
    public sealed class LessThanEqual : BF
    {
        public override bool Func(byte Val1, byte Val2) => Val1 <= Val2;
    }

    [Chip("ByteGreaterThan")]
    [Serializable]
    public sealed class GreaterThan : BF
    {
        public override bool Func(byte Val1, byte Val2) => Val1 > Val2;
    }

    [Chip("ByteGreaterThanEqual")]
    [Serializable]
    public sealed class GreaterThanEqual : BF
    {
        public override bool Func(byte Val1, byte Val2) => Val1 >= Val2;
    }
}
