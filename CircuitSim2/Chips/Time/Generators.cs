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
        public TickCount(Engine.Engine Engine = null) : base(Engine)
        {
        }

        protected override int NextValue()
        {
            return Environment.TickCount;
        }
    }
}
