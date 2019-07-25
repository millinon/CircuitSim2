using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<double, double, double>;
using UF = CircuitSim2.Chips.Functors.UnaryFunctor<double, double>;

namespace CircuitSim2.Chips.Double.Arithmetic
{
    [Chip("DoubleAdd")]
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base((a, b) => (a + b), Engine)
        {

        }
    }

    [Chip("DoubleSubtract")]
    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base((a, b) => (a - b), Engine)
        {

        }
    }

    [Chip("DoubleMultiply")]
    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base((a, b) => (a * b), Engine)
        {

        }
    }

    [Chip("DoubleDivide")]
    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base((a, b) => (a / b), Engine)
        {

        }
    }

    [Chip("DoubleModulus")]
    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base((a, b) => (a % b), Engine)
        {

        }
    }

    [Chip("DoubleSin")]
    public sealed class Sin : UF
    {
        public Sin(Engine.Engine Engine = null) : base(Math.Sin, Engine)
        {

        }
    }

    [Chip("DoubleSinh")]
    public sealed class Sinh : UF
    {
        public Sinh(Engine.Engine Engine = null) : base(Math.Sinh, Engine)
        {

        }
    }

    [Chip("DoubleCos")]
    public sealed class Cos : UF
    {
        public Cos(Engine.Engine Engine = null) : base(Math.Cos, Engine)
        {

        }
    }

    [Chip("DoubleCosh")]
    public sealed class Cosh : UF
    {
        public Cosh(Engine.Engine Engine = null) : base(Math.Cosh, Engine)
        {

        }
    }

    [Chip("DoubleTan")]
    public sealed class Tan : UF
    {
        public Tan(Engine.Engine Engine = null) : base(Math.Tan, Engine)
        {

        }
    }

    [Chip("DoubleTanh")]
    public sealed class Tanh : UF
    {
        public Tanh(Engine.Engine Engine = null) : base(Math.Tanh, Engine)
        {

        }
    }

    [Chip("DoublePower")]
    public sealed class Power : BF
    {
        public Power(Engine.Engine Engine = null) : base(Math.Pow, Engine)
        {

        }
    }

    [Chip("DoubleMin")]
    public sealed class Min : BF
    {
        public Min(Engine.Engine Engine = null) : base(Math.Min, Engine)
        {

        }
    }

    [Chip("DoubleMax")]
    public sealed class Max : BF
    {
        public Max(Engine.Engine Engine = null) : base(Math.Max, Engine)
        {

        }
    }
}
