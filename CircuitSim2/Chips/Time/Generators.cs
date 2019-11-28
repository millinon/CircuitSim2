using CircuitSim2.Chips.Functors;
using System;

namespace CircuitSim2.Chips.Time
{
    [Chip("TickCount")]
    [Serializable]
    public class TickCount : Generator<int>
    {
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
