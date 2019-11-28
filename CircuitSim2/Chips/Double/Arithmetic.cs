using CircuitSim2.Chips.Functors;
using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<double, double, double>;
using UF = CircuitSim2.Chips.Functors.UnaryFunctor<double, double>;

namespace CircuitSim2.Chips.Double.Arithmetic
{
    [Chip("DoubleAdd")]
    [Serializable]
    public sealed class Add : BF
    {
        public override double Func(double Val1, double Val2) => Val1 + Val2;
    }

    [Chip("DoubleSubtract")]
    [Serializable]
    public sealed class Subtract : BF
    {
        public override double Func(double Val1, double Val2) => Val1 - Val2;
    }

    [Chip("DoubleMultiply")]
    [Serializable]
    public sealed class Multiply : BF
    {

        public override double Func(double Val1, double Val2) => Val1 * Val2;
    }

    [Chip("DoubleDivide")]
    [Serializable]
    public sealed class Divide : BF
    {
        public override double Func(double Val1, double Val2) => Val1 / Val2;
    }

    [Chip("DoubleModulus")]
    [Serializable]
    public sealed class Modulus : BF
    {
        public override double Func(double Val1, double Val2) => Val1 % Val2;
    }

    [Chip("DoubleMin")]
    [Serializable]
    public sealed class Min : BF
    {

        public override double Func(double Val1, double Val2) => Math.Min(Val1, Val2);
    }

    [Chip("DoubleMax")]
    [Serializable]
    public sealed class Max : BF
    {
        public override double Func(double Val1, double Val2) => Math.Max(Val1, Val2);
    }

    [Chip("DoubleSin")]
    [Serializable]
    public sealed class Sin : UF
    {
        public override double Func(double Value) => Math.Sin(Value);
    }

    [Chip("DoubleSinh")]
    [Serializable]
    public sealed class Sinh : UF
    {
        public override double Func(double Value) => Math.Sinh(Value);
    }

    [Chip("DoubleCos")]
    [Serializable]
    public sealed class Cos : UF
    {
        public override double Func(double Value) => Math.Cos(Value);
    }

    [Chip("DoubleCosh")]
    [Serializable]
    public sealed class Cosh : UF
    {
        public override double Func(double Value) => Math.Cos(Value);
    }

    [Chip("DoubleTan")]
    [Serializable]
    public sealed class Tan : UF
    {
        public override double Func(double Value) => Math.Tan(Value);
    }

    [Chip("DoubleTanh")]
    [Serializable]
    public sealed class Tanh : UF
    {
        public override double Func(double Value) => Math.Tanh(Value);
    }

    [Chip("DoublePower")]
    [Serializable]
    public sealed class Power : BF
    {
        public override double Func(double Val1, double Val2) => Math.Pow(Val1, Val2);
    }

    [Chip("DoubleClamp")]
    [Serializable]
    public sealed class Clamp : Clamp<double>
    {
    }
}
