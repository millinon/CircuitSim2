using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<int, int, int>;

namespace CircuitSim2.Chips.Integer.Arithmetic
{
    [PureChip("IntegerAdd")]
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base((a, b) => (a + b), Engine)
        {

        }
    }

    [PureChip("IntegerSubtract")]
    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base((a, b) => (a - b), Engine)
        {

        }
    }

    [PureChip("IntegerMultiply")]
    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base((a, b) => (a * b), Engine)
        {

        }
    }

    [PureChip("IntegerDivide")]
    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base((a, b) => (a / b), Engine)
        {

        }
    }

    [PureChip("IntegerModulus")]
    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base((a, b) => (a % b), Engine)
        {

        }
    }

    [PureChip("IntegerLeftShift")]
    public sealed class LeftShift : BF
    {
        public LeftShift(Engine.Engine Engine = null) : base((a, b) => (a << b), Engine)
        {

        }
    }

    [PureChip("IntegerRightShift")]
    public sealed class RightShift : BF
    {
        public RightShift(Engine.Engine Engine = null) : base((a, b) => (a >> b), Engine)
        {

        }
    }

    [PureChip("IntegerMin")]
    public sealed class Min : BF
    {
        public Min(Engine.Engine Engine = null) : base(Math.Min, Engine)
        {

        }
    }

    [PureChip("IntegerMax")]
    public sealed class Max : BF
    {
        public Max(Engine.Engine Engine = null) : base(Math.Max, Engine)
        {

        }
    }
}
