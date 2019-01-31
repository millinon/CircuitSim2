using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<float, float, float>;

namespace CircuitSim2.Chips.Single.Arithmetic
{
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base("SingleAdd", (a, b) => (a + b), Engine)
        {

        }
    }

    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base("SingleSubtract", (a, b) => (a - b), Engine)
        {

        }
    }

    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base("SingleMultiply", (a, b) => (a * b), Engine)
        {

        }
    }

    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base("SingleDivide", (a, b) => (a/b), Engine)
        {

        }
    }

    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base("SingleModulus", (a, b) => (a%b), Engine)
        {

        }
    }

    public sealed class Min : BF
    {
        public Min(Engine.Engine Engine = null) : base("SingleMin", Math.Min, Engine)
        {
            
        }
    }

    public sealed class Max : BF
    {
        public Max(Engine.Engine Engine = null) : base("SingleMax", Math.Max, Engine)
        {

        }
    }
}
