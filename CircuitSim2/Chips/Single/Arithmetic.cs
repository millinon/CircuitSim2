using CircuitSim2.Chips.Functors;
using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<float, float, float>;

namespace CircuitSim2.Chips.Single.Arithmetic
{
    [Chip("SingleAdd")]
    [Serializable]
    public sealed class Add : BF
    {
        public override float Func(float Val1, float Val2) => Val1 + Val2;
    }

    [Chip("SingleSubtract")]
    [Serializable]
    public sealed class Subtract : BF
    {
        public override float Func(float Val1, float Val2) => Val1 - Val2;
    }

    [Chip("SingleMultiply")]
    [Serializable]
    public sealed class Multiply : BF
    {
        public override float Func(float Val1, float Val2) => Val1 * Val2;
    }

    [Chip("SingleDivide")]
    [Serializable]
    public sealed class Divide : BF
    {
        public override float Func(float Val1, float Val2) => Val1 / Val2;
    }

    [Chip("SingleModulus")]
    [Serializable]
    public sealed class Modulus : BF
    {
        public override float Func(float Val1, float Val2) => Val1 % Val2;
    }

    [Chip("SingleMin")]
    [Serializable]
    public sealed class Min : BF
    {
        public override float Func(float Val1, float Val2) => Math.Min(Val1, Val2);
    }

    [Chip("SingleMax")]
    [Serializable]
    public sealed class Max : BF
    {
        public override float Func(float Val1, float Val2) => Math.Max(Val1, Val2);
    }

    [Chip("SingleClamp")]
    [Serializable]
    public sealed class Clamp : Clamp<float>
    {
    }
}
