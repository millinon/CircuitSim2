using System;
using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.DateTime.Generators
{
    [Chip("DateTimeConstant")]
    public sealed class Constant : Constant<System.DateTime>
    {
        public Constant(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Constant(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Constant(Engine.Engine Engine) : this(null, Engine)
        {
        }
    }

    [Chip("DateTimeNow")]
    public sealed class Now : Generator<System.DateTime>
    {
        public Now(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Now(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Now(Engine.Engine Engine) : this(null, Engine)
        {
        }

        protected override System.DateTime NextValue() => System.DateTime.Now;
    }
}