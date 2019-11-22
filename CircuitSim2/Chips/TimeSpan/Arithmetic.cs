using CircuitSim2.Chips.Functors;
using System;

namespace CircuitSim2.Chips.TimeSpan.Arithmetic
{
    [Chip("TimeSpanAdd")]
    public sealed class Add : BinaryFunctor<System.TimeSpan, System.TimeSpan, System.TimeSpan>
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

        public override System.TimeSpan Func(System.TimeSpan Val1, System.TimeSpan Val2) => Val1 + Val2;
    }

    [Chip("TimeSpanSubtract")]
    public sealed class Subtract : BinaryFunctor<System.TimeSpan, System.TimeSpan, System.TimeSpan>
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

        public override System.TimeSpan Func(System.TimeSpan Val1, System.TimeSpan Val2) => Val1 + Val2;
    }
}
