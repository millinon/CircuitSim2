using CircuitSim2.Chips.Functors;
using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<byte, byte, byte>;

namespace CircuitSim2.Chips.Byte.Arithmetic
{
    [Chip("ByteAdd")]
    [Serializable]
    public sealed class Add : BF
    {
        public override byte Func(byte Val1, byte Val2) => (byte)(Val1 + Val2);
    }

    [Chip("ByteSubtract")]
    [Serializable]
    public sealed class Subtract : BF
    {
        public override byte Func(byte Val1, byte Val2) => (byte)(Val1 - Val2);
    }

    [Chip("ByteMultiply")]
    [Serializable]
    public sealed class Multiply : BF
    {
        public override byte Func(byte Val1, byte Val2) => (byte)(Val1 * Val2);
    }

    [Chip("ByteDivide")]
    [Serializable]
    public sealed class Divide : BF
    {
        public override byte Func(byte Val1, byte Val2) => (byte)(Val1 / Val2);
    }

    [Chip("ByteModulus")]
    [Serializable]
    public sealed class Modulus : BF
    {
        public override byte Func(byte Val1, byte Val2) => (byte)(Val1 % Val2);
    }

    [Chip("ByteMin")]
    [Serializable]
    public sealed class Min : BF
    {
        public override byte Func(byte Val1, byte Val2) => Math.Min(Val1, Val2);
    }

    [Chip("ByteMax")]
    [Serializable]
    public sealed class Max : BF
    {
        public override byte Func(byte Val1, byte Val2) => Math.Max(Val1, Val2);
    }

    [Chip("ByteClamp")]
    [Serializable]
    public sealed class Clamp : Clamp<byte>
    {
    }
}