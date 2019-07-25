using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<byte, byte, byte>;

namespace CircuitSim2.Chips.Byte.Arithmetic
{
    [Chip("ByteAdd")]
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base((a, b) => (byte)(a + b), Engine)
        {

        }
    }

    [Chip("ByteSubtract")]
    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base((a, b) => (byte)(a - b), Engine)
        {

        }
    }

    [Chip("ByteMultiply")]
    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base((a, b) => (byte)(a * b), Engine)
        {

        }
    }

    [Chip("ByteDivide")]
    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base((a, b) => (byte)(a / b), Engine)
        {

        }
    }

    [Chip("ByteModulus")]
    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base((a, b) => (byte)(a % b), Engine)
        {

        }
    }

    [Chip("ByteLeftShift")]
    public sealed class LeftShift : BF
    {
        public LeftShift(Engine.Engine Engine = null) : base((a, b) => (byte)(a << b), Engine)
        {

        }
    }

    [Chip("ByteRightShift")]
    public sealed class RightShift : BF
    {
        public RightShift(Engine.Engine Engine = null) : base((a, b) => (byte)(a >> b), Engine)
        {

        }
    }

    [Chip("ByteMin")]
    public sealed class Min : BF
    {
        public Min(Engine.Engine Engine = null) : base(Math.Min, Engine)
        {

        }
    }

    [Chip("ByteMax")]
    public sealed class Max : BF
    {
        public Max(Engine.Engine Engine = null) : base(Math.Max, Engine)
        {

        }
    }
}
