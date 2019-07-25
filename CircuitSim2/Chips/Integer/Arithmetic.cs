using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<int, int, int>;

namespace CircuitSim2.Chips.Integer.Arithmetic
{
    [Chip("IntegerAdd")]
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base((a, b) => (a + b), Engine)
        {

        }
    }

    [Chip("IntegerSubtract")]
    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base((a, b) => (a - b), Engine)
        {

        }
    }

    [Chip("IntegerMultiply")]
    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base((a, b) => (a * b), Engine)
        {

        }
    }

    [Chip("IntegerDivide")]
    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base((a, b) => (a / b), Engine)
        {

        }
    }

    [Chip("IntegerModulus")]
    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base((a, b) => (a % b), Engine)
        {

        }
    }

    [Chip("IntegerLeftShift")]
    public sealed class LeftShift : BF
    {
        public LeftShift(Engine.Engine Engine = null) : base((a, b) => (a << b), Engine)
        {

        }
    }

    [Chip("IntegerRightShift")]
    public sealed class RightShift : BF
    {
        public RightShift(Engine.Engine Engine = null) : base((a, b) => (a >> b), Engine)
        {

        }
    }

    [Chip("IntegerMin")]
    public sealed class Min : BF
    {
        public Min(Engine.Engine Engine = null) : base(Math.Min, Engine)
        {

        }
    }

    [Chip("IntegerMax")]
    public sealed class Max : BF
    {
        public Max(Engine.Engine Engine = null) : base(Math.Max, Engine)
        {

        }
    }
}
