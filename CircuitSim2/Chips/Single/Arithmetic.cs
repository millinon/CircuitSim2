using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<float, float, float>;

namespace CircuitSim2.Chips.Single.Arithmetic
{
    [Chip("SingleAdd")]
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base((a, b) => (a + b), Engine)
        {

        }
    }

    [Chip("SingleSubtract")]
    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base((a, b) => (a - b), Engine)
        {

        }
    }

    [Chip("SingleMultiply")]
    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base((a, b) => (a * b), Engine)
        {

        }
    }

    [Chip("SingleDivide")]
    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base((a, b) => (a / b), Engine)
        {

        }
    }

    [Chip("SingleModulus")]
    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base((a, b) => (a % b), Engine)
        {

        }
    }

    [Chip("SingleMin")]
    public sealed class Min : BF
    {
        public Min(Engine.Engine Engine = null) : base(Math.Min, Engine)
        {

        }
    }

    [Chip("SingleMax")]
    public sealed class Max : BF
    {
        public Max(Engine.Engine Engine = null) : base(Math.Max, Engine)
        {

        }
    }
}
