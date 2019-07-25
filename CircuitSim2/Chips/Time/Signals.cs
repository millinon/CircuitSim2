using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CircuitSim2.Engine;
using CircuitSim2.IO;

namespace CircuitSim2.Chips.Time
{
    [Chip("Clock")]
    public class Clock : ChipBase
    {
        public ulong Period;

        private object ticks_lock_obj;
        private bool Pulse;
        private ulong Ticks;

        public readonly GenericOutput<bool> Outputs;

        private Clock(ulong Period, ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            this.Period = Period;

            InputSet = new CircuitSim2.IO.NoInputs();
            OutputSet = (Outputs = new GenericOutput<bool>(this));

            ticks_lock_obj = new object();
            Ticks = 0;
        }

        private Clock(ulong Period, ChipBase ParentChip) : this(Period, ParentChip, ParentChip?.Engine)
        {

        }

        public Clock(ulong Period, Engine.Engine Engine) : this(Period, null, Engine)
        {

        }

        public Clock(ulong Period) : this(Period, null, null)
        {

        }
     
        public void Step()
        {
            lock (ticks_lock_obj)
            {
                if (Pulse)
                {
                    Pulse = false;
                    Outputs.Out.Value = false;
                }

                Ticks++;

                if(Ticks == Period)
                {
                    Ticks = 0;
                    Pulse = true;
                    Outputs.Out.Value = true;
                }
            }
        }
    }
}
