using CircuitSim2.Chips.Functors;
using System;

namespace CircuitSim2.Chips.DateTime.Arithmetic
{
    [Chip("DateTimeAddSpan")]
    public sealed class AddSpan : BinaryFunctor<System.DateTime, System.TimeSpan, System.DateTime>
    {
        public AddSpan(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public AddSpan(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public AddSpan(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override System.DateTime Func(System.DateTime Val1, System.TimeSpan Val2) => Val1 + Val2;
    }

    [Chip("DateTimeSubtract")]
    public sealed class Subtract : BinaryFunctor<System.DateTime, System.DateTime, System.TimeSpan>
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

        public override System.TimeSpan Func(System.DateTime Val1, System.DateTime Val2) => Val1 - Val2;
    }

    [Chip("DateTimeSubtractSpan")]
    public sealed class SubtractSpan : BinaryFunctor<System.DateTime, System.TimeSpan, System.DateTime>
    {
        public SubtractSpan(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public SubtractSpan(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public SubtractSpan(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override System.DateTime Func(System.DateTime Val1, System.TimeSpan Val2) => Val1 - Val2;
    }
}
