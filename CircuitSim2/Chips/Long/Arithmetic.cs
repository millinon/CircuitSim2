using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<long, long, long>;

namespace CircuitSim2.Chips.Long.Arithmetic
{
    [Chip("LongAdd")]
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base((a, b) => (a + b), Engine)
        {

        }
    }

    [Chip("LongSubtract")]
    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base((a, b) => (a - b), Engine)
        {

        }
    }

    [Chip("LongMultiply")]
    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base((a, b) => (a * b), Engine)
        {

        }
    }

    [Chip("LongDivide")]
    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base((a, b) => (a / b), Engine)
        {

        }
    }

    [Chip("LongModulus")]
    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base((a, b) => (a % b), Engine)
        {

        }
    }

    [Chip("LongMin")]
    public sealed class Min : BF
    {
        public Min(Engine.Engine Engine = null) : base(Math.Min, Engine)
        {

        }
    }

    [Chip("LongMax")]
    public sealed class Max : BF
    {
        public Max(Engine.Engine Engine = null) : base(Math.Max, Engine)
        {

        }
    }
}
