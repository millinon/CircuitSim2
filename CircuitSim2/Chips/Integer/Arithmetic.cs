using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<int, int, int>;

namespace CircuitSim2.Chips.Integer.Arithmetic
{
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base("IntegerAdd", (a, b) => (a + b), Engine)
        {

        }
    }

    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base("IntegerSubtract", (a, b) => (a - b), Engine)
        {

        }
    }

    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base("IntegerMultiply", (a, b) => (a * b), Engine)
        {

        }
    }

    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base("IntegerDivide", (a, b) => (a/b), Engine)
        {

        }
    }

    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base("IntegerModulus", (a, b) => (a%b), Engine)
        {

        }
    }

    public sealed class LeftShift : BF
    {
        public LeftShift(Engine.Engine Engine = null) : base("IntegerLeftShift", (a, b) => (a << b), Engine)
        {

        }
    }

    public sealed class RightShift : BF
    {
        public RightShift(Engine.Engine Engine = null) : base("IntegerRightShift", (a, b) => (a >> b), Engine)
        {

        }
    }
    
    public sealed class Min : BF
    {
        public Min(Engine.Engine Engine = null) : base("IntegerMin", Math.Min, Engine)
        {
            
        }
    }

    public sealed class Max : BF
    {
        public Max(Engine.Engine Engine = null) : base("IntegerMax", Math.Max, Engine)
        {

        }
    }
}
