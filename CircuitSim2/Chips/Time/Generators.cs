using CircuitSim2.Chips.Functors;
using System;

namespace CircuitSim2.Chips.Time
{
    [Chip("TickCount")]
    public class TickCount : Generator<int>
    {
        public TickCount(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public TickCount(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public TickCount(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        protected override int NextValue()
        {
            return Environment.TickCount;
        }

        public override SizeVec size => new SizeVec
        {
            Length = 1,
            Width = 2,
            Height = 1,
        };
    }
}
