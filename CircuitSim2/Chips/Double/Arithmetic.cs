using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<double, double, double>;
using UF = CircuitSim2.Chips.Functors.UnaryFunctor<double, double>;

namespace CircuitSim2.Chips.Double.Arithmetic
{
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base("DoubleAdd", (a, b) => (a + b), Engine)
        {

        }
    }

    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base("DoubleSubtract", (a, b) => (a - b), Engine)
        {

        }
    }

    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base("DoubleMultiply", (a, b) => (a * b), Engine)
        {

        }
    }

    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base("DoubleDivide", (a, b) => (a/b), Engine)
        {

        }
    }

    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base("DoubleModulus", (a, b) => (a%b), Engine)
        {

        }
    }

    public sealed class Sin : UF
    {
        public Sin(Engine.Engine Engine = null) : base("DoubleSin", Math.Sin, Engine)
        {

        }
    }

    public sealed class Sinh : UF
    {
        public Sinh(Engine.Engine Engine = null) : base("DoubleSinh", Math.Sinh, Engine)
        {

        }
    }

    public sealed class Cos : UF
    {
        public Cos(Engine.Engine Engine = null) : base("DoubleCos", Math.Cos, Engine)
        {

        }
    }

    public sealed class Cosh : UF
    {
        public Cosh(Engine.Engine Engine = null) : base("DoubleCosh", Math.Cosh, Engine)
        {

        }
    }

    public sealed class Tan : UF
    {
        public Tan(Engine.Engine Engine = null) : base("DoubleTan", Math.Tan, Engine)
        {

        }
    }

    public sealed class Tanh : UF
    {
        public Tanh(Engine.Engine Engine = null) : base("DoubleTanh", Math.Tanh, Engine)
        {

        }
    }

    public sealed class Power : BF
    {
        public Power(Engine.Engine Engine = null) : base("DoublePower", Math.Pow, Engine)
        {

        }
    }

    public sealed class Min : BF
    {
        public Min(Engine.Engine Engine = null) : base("DoubleMin", Math.Min, Engine)
        {
            
        }
    }

    public sealed class Max : BF
    {
        public Max(Engine.Engine Engine = null) : base("DoubleMax", Math.Max, Engine)
        {

        }
    }
}
