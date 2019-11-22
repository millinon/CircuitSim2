using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<double, double, double>;
using UF = CircuitSim2.Chips.Functors.UnaryFunctor<double, double>;

namespace CircuitSim2.Chips.Double.Arithmetic
{
    [Chip("DoubleAdd")]
    public sealed class Add : BF
    {
        public Add(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Add(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Add(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Val1, double Val2) => Val1 + Val2;
    }

    [Chip("DoubleSubtract")]
    public sealed class Subtract : BF
    {
        public Subtract(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Subtract(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Subtract(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Val1, double Val2) => Val1 - Val2;
    }

    [Chip("DoubleMultiply")]
    public sealed class Multiply : BF
    {
        public Multiply(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Multiply(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Multiply(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Val1, double Val2) => Val1 * Val2;
    }

    [Chip("DoubleDivide")]
    public sealed class Divide : BF
    {
        private Divide(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Divide(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Divide(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Val1, double Val2) => Val1 / Val2;
    }

    [Chip("DoubleModulus")]
    public sealed class Modulus : BF
    {
        public Modulus(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Modulus(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Modulus(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Val1, double Val2) => Val1 % Val2;
    }

    [Chip("DoubleMin")]
    public sealed class Min : BF
    {
        public Min(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Min(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Min(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Val1, double Val2) => Math.Min(Val1, Val2);
    }

    [Chip("DoubleMax")]
    public sealed class Max : BF
    {
        public Max(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Max(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Max(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Val1, double Val2) => Math.Max(Val1, Val2);
    }

    [Chip("DoubleSin")]
    public sealed class Sin : UF
    {
        public Sin(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Sin(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Sin(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Value) => Math.Sin(Value);
    }

    [Chip("DoubleSinh")]
    public sealed class Sinh : UF
    {
        public Sinh(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Sinh(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Sinh(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Value) => Math.Sinh(Value);
    }

    [Chip("DoubleCos")]
    public sealed class Cos : UF
    {
        public Cos(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Cos(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Cos(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Value) => Math.Cos(Value);
    }

    [Chip("DoubleCosh")]
    public sealed class Cosh : UF
    {
        public Cosh(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Cosh(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Cosh(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Value) => Math.Cos(Value);
    }

    [Chip("DoubleTan")]
    public sealed class Tan : UF
    {
        public Tan(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Tan(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Tan(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Value) => Math.Tan(Value);
    }

    [Chip("DoubleTanh")]
    public sealed class Tanh : UF
    {
        public Tanh(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Tanh(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Tanh(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Value) => Math.Tanh(Value);
    }

    [Chip("DoublePower")]
    public sealed class Power : BF
    {
        public Power(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Power(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Power(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(double Val1, double Val2) => Math.Pow(Val1, Val2);
    }
}
