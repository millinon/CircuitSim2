using CircuitSim2.Chips.Functors;
using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<int, int, int>;

namespace CircuitSim2.Chips.Integer.Arithmetic
{
    [Chip("IntegerAdd")]
    [Serializable]
    public sealed class Add : BF
    {
        public override int Func(int Val1, int Val2) => Val1 + Val2;
    }

    [Chip("IntegerSubtract")]
    [Serializable]
    public sealed class Subtract : BF
    {
        public override int Func(int Val1, int Val2) => Val1 - Val2;
    }

    [Chip("IntegerMultiply")]
    [Serializable]
    public sealed class Multiply : BF
    {
        public override int Func(int Val1, int Val2) => Val1 * Val2;
    }

    [Chip("IntegerDivide")]
    [Serializable]
    public sealed class Divide : BF
    {
        public override int Func(int Val1, int Val2) => Val1 / Val2;
    }

    [Chip("IntegerModulus")]
    [Serializable]
    public sealed class Modulus : BF
    {
        public override int Func(int Val1, int Val2) => Val1 % Val2;
    }

    [Chip("IntegerMin")]
    [Serializable]
    public sealed class Min : BF
    {
        public override int Func(int Val1, int Val2) => Math.Min(Val1, Val2);
    }

    [Chip("IntegerMax")]
    [Serializable]
    public sealed class Max : BF
    {
        public override int Func(int Val1, int Val2) => Math.Max(Val1, Val2);
    }

    [Chip("IntegerClamp")]
    [Serializable]
    public sealed class Clamp : Clamp<int>
    {
    }
}
