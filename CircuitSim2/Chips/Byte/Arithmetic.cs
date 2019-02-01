using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<byte, byte, byte>;

namespace CircuitSim2.Chips.Byte.Arithmetic
{
    [PureChip("ByteAdd")]
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base((a, b) => (byte)(a + b), Engine)
        {

        }
    }

    [PureChip("ByteSubtract")]
    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base((a, b) => (byte)(a - b), Engine)
        {

        }
    }

    [PureChip("ByteMultiply")]
    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base((a, b) => (byte)(a * b), Engine)
        {

        }
    }

    [PureChip("ByteDivide")]
    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base((a, b) => (byte)(a / b), Engine)
        {

        }
    }

    [PureChip("ByteModulus")]
    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base((a, b) => (byte)(a % b), Engine)
        {

        }
    }

    [PureChip("ByteLeftShift")]
    public sealed class LeftShift : BF
    {
        public LeftShift(Engine.Engine Engine = null) : base((a, b) => (byte)(a << b), Engine)
        {

        }
    }

    [PureChip("ByteRightShift")]
    public sealed class RightShift : BF
    {
        public RightShift(Engine.Engine Engine = null) : base((a, b) => (byte)(a >> b), Engine)
        {

        }
    }

    [PureChip("ByteMin")]
    public sealed class Min : BF
    {
        public Min(Engine.Engine Engine = null) : base(Math.Min, Engine)
        {

        }
    }

    [PureChip("ByteMax")]
    public sealed class Max : BF
    {
        public Max(Engine.Engine Engine = null) : base(Math.Max, Engine)
        {

        }
    }
}
