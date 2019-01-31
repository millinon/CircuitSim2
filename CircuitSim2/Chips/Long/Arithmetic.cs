using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<long, long, long>;

namespace CircuitSim2.Chips.Long.Arithmetic
{
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base("LongAdd", (a, b) => (a + b), Engine)
        {

        }
    }

    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base("LongSubtract", (a, b) => (a - b), Engine)
        {

        }
    }

    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base("LongMultiply", (a, b) => (a * b), Engine)
        {

        }
    }

    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base("LongDivide", (a, b) => (a/b), Engine)
        {

        }
    }

    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base("LongModulus", (a, b) => (a%b), Engine)
        {

        }
    }
    
    public sealed class Min : BF
    {
        public Min(Engine.Engine Engine = null) : base("LongMin", Math.Min, Engine)
        {
            
        }
    }

    public sealed class Max : BF
    {
        public Max(Engine.Engine Engine = null) : base("LongMax", Math.Max, Engine)
        {

        }
    }
}
