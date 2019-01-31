using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<byte, byte, byte>;

namespace CircuitSim2.Chips.Byte.Arithmetic
{
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base("ByteAdd", (a, b) => (byte)(a + b), Engine)
        {

        }
    }

    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base("ByteSubtract", (a, b) => (byte)(a - b), Engine)
        {

        }
    }

    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base("ByteMultiply", (a, b) => (byte)(a * b), Engine)
        {

        }
    }

    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base("ByteDivide", (a, b) => (byte)(a/b), Engine)
        {

        }
    }

    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base("ByteModulus", (a, b) => (byte)(a%b), Engine)
        {

        }
    }

    public sealed class LeftShift : BF
    {
        public LeftShift(Engine.Engine Engine = null) : base("ByteLeftShift", (a, b) => (byte)(a << b), Engine)
        {

        }
    }

    public sealed class RightShift : BF
    {
        public RightShift(Engine.Engine Engine = null) : base("ByteRightShift", (a, b) => (byte)(a >> b), Engine)
        {

        }
    }
    
    public sealed class Min : BF
    {
        public Min(Engine.Engine Engine = null) : base("ByteMin", Math.Min, Engine)
        {
            
        }
    }

    public sealed class Max : BF
    {
        public Max(Engine.Engine Engine = null) : base("ByteMax", Math.Max, Engine)
        {

        }
    }
}
