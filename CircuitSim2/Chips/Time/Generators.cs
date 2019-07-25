using CircuitSim2.Chips.Functors;
using CircuitSim2.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSim2.Chips.Time
{
    [Chip("TickCount")]
    public class TickCount : Generator<int>
    {
        private TickCount(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public TickCount(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public TickCount(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public TickCount() : this(null, null)
        {

        }

        protected override int NextValue()
        {
            return Environment.TickCount;
        }
    }
}
