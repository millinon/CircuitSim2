﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSim2.Chips.DateTime.Inputs
{
    [Chip("DateTimeSwitch")]
    public sealed class Switch : IO.Inputs.Switch<System.DateTime>
    {
        public Switch(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Switch(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Switch(Engine.Engine Engine) : this(null, Engine)
        {
        }
    }
}