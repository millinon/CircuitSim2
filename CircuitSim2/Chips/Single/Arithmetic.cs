using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<float, float, float>;

namespace CircuitSim2.Chips.Single.Arithmetic
{
    [Chip("SingleAdd")]
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

        public override float Func(float Val1, float Val2) => Val1 + Val2;
    }

    [Chip("SingleSubtract")]
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

        public Subtract() : this(null, null)
        {
        }

        public override float Func(float Val1, float Val2) => Val1 - Val2;
    }

    [Chip("SingleMultiply")]
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

        public override float Func(float Val1, float Val2) => Val1 * Val2;
    }

    [Chip("SingleDivide")]
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

        public override float Func(float Val1, float Val2) => Val1 / Val2;
    }

    [Chip("SingleModulus")]
    public sealed class Modulus : BF
    {
        private Modulus(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Modulus(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Modulus(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override float Func(float Val1, float Val2) => Val1 % Val2;
    }

    [Chip("SingleMin")]
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

        public override float Func(float Val1, float Val2) => Math.Min(Val1, Val2);
    }

    [Chip("SingleMax")]
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

        public override float Func(float Val1, float Val2) => Math.Max(Val1, Val2);
    }
}
