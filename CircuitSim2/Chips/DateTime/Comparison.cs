﻿using BF = CircuitSim2.Chips.Functors.BinaryFunctor<System.DateTime, System.DateTime, bool>;

namespace CircuitSim2.Chips.DateTime.Comparison
{
    [Chip("DateTimeEqual")]
    public sealed class Equal : BF
    {
        public Equal(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Equal(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Equal(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override bool Func(System.DateTime Value1, System.DateTime Value2) => Value1 == Value2;
    }

    [Chip("DateTimeNotEqual")]
    public sealed class NotEqual : BF
    {
        public NotEqual(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public NotEqual(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public NotEqual(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override bool Func(System.DateTime Val1, System.DateTime Val2) => Val1 != Val2;
    }

    [Chip("DateTimeLessThan")]
    public sealed class LessThan : BF
    {
        public LessThan(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public LessThan(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public LessThan(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override bool Func(System.DateTime Val1, System.DateTime Val2) => System.DateTime.Compare(Val1, Val2) < 0;
    }

    [Chip("DateTimeLessThanEqual")]
    public sealed class LessThanEqual : BF
    {
        public LessThanEqual(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public LessThanEqual(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public LessThanEqual(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override bool Func(System.DateTime Val1, System.DateTime Val2) => System.DateTime.Compare(Val1, Val2) <= 0;
    }

    [Chip("DateTimeGreaterThan")]
    public sealed class GreaterThan : BF
    {
        public GreaterThan(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public GreaterThan(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public GreaterThan(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override bool Func(System.DateTime Val1, System.DateTime Val2) => System.DateTime.Compare(Val1, Val2) > 0;
    }

    [Chip("DateTimeGreaterThanEqual")]
    public sealed class GreaterThanEqual : BF
    {
        public GreaterThanEqual(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public GreaterThanEqual(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public GreaterThanEqual(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override bool Func(System.DateTime Val1, System.DateTime Val2) => System.DateTime.Compare(Val1, Val2) >= 0;
    }

    [Chip("DateTimeCompare")]
    public sealed class Compare : Chips.Functors.BinaryFunctor<System.DateTime, System.DateTime, int>
    {
        public Compare(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Compare(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Compare(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override int Func(System.DateTime Val1, System.DateTime Val2) => System.DateTime.Compare(Val1, Val2);
    }
}