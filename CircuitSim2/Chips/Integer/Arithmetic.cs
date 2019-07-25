using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<int, int, int>;

namespace CircuitSim2.Chips.Integer.Arithmetic
{
    [Chip("IntegerAdd")]
    public sealed class Add : BF
    {
        private Add(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Add(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Add(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public Add() : this(null, null)
        {
        }

        public override int Func(int Val1, int Val2) => Val1 + Val2;
    }

    [Chip("IntegerSubtract")]
    public sealed class Subtract : BF
    {
        private Subtract(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
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

        public override int Func(int Val1, int Val2) => Val1 - Val2;
    }

    [Chip("IntegerMultiply")]
    public sealed class Multiply : BF
    {
        private Multiply(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Multiply(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Multiply(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public Multiply() : this(null, null)
        {
        }

        public override int Func(int Val1, int Val2) => Val1 * Val2;
    }

    [Chip("IntegerDivide")]
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

        public Divide() : this(null, null)
        {
        }

        public override int Func(int Val1, int Val2) => Val1 / Val2;
    }

    [Chip("IntegerModulus")]
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

        public Modulus() : this(null, null)
        {
        }

        public override int Func(int Val1, int Val2) => Val1 % Val2;
    }

    [Chip("IntegerMin")]
    public sealed class Min : BF
    {
        private Min(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Min(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Min(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public Min() : this(null, null)
        {
        }

        public override int Func(int Val1, int Val2) => Math.Min(Val1, Val2);
    }

    [Chip("IntegerMax")]
    public sealed class Max : BF
    {
        private Max(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Max(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Max(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public Max() : this(null, null)
        {
        }
        public override int Func(int Val1, int Val2) => Math.Max(Val1, Val2);
    }
}
