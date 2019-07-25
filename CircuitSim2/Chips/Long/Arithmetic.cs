using System;

using BF = CircuitSim2.Chips.Functors.BinaryFunctor<long, long, long>;

namespace CircuitSim2.Chips.Long.Arithmetic
{
    [Chip("LongAdd")]
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

        public override long Func(long Val1, long Val2) => Val1 + Val2;
    }

    [Chip("LongSubtract")]
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

        public override long Func(long Val1, long Val2) => Val1 - Val2;
    }

    [Chip("LongMultiply")]
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

        public override long Func(long Val1, long Val2) => Val1 * Val2;
    }

    [Chip("LongDivide")]
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

        public override long Func(long Val1, long Val2) => Val1 / Val2;
    }

    [Chip("LongModulus")]
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

        public override long Func(long Val1, long Val2) => Val1 % Val2;
    }

    [Chip("LongMin")]
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

        public override long Func(long Val1, long Val2) => Math.Min(Val1, Val2);
    }

    [Chip("LongMax")]
    public sealed class Max : BF
    {
        private  Max(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public  Max(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public  Max(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public  Max() : this(null, null)
        {
        }
        public override long Func(long Val1, long Val2) => Math.Max(Val1, Val2);
    }
}
