using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CircuitSim2.Engine;
using CircuitSim2.IO;

namespace CircuitSim2.Chips.Time
{
    public abstract class Clock : ChipBase
    {
        public readonly GenericOutput<bool> Outputs;

        public Clock(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = new CircuitSim2.IO.NoInputs();

            OutputSet = (Outputs = new GenericOutput<bool>(this));
        }

        public abstract void Update();

        protected bool did_pulse
        {
            get; private set;
        } = false;

        public abstract bool Pulse { get; }

        private bool _out;
        public sealed override void Compute()
        {
            _out = Pulse;
        }

        public sealed override void Commit()
        {
            Outputs.Out.Value = _out;
            did_pulse = _out;
        }

        public sealed override void Tick()
        {
            base.Tick();
        }
    }

    [Chip("SteppingClock")]
    public sealed class SteppingClock : Clock
    {
        private ulong period = 1;
        [ChipProperty]
        public ulong Period
        {
            get
            {
                return period;
            }
            set
            {
                period = value;

                if (AutoTick)
                {
                    Tick();
                }
            }
        }


        private object ticks_lock_obj = new object();
        private ulong Ticks = 0;
        private bool pulse = false;

        public override bool Pulse => pulse;

        public override void Update()
        {
            lock (ticks_lock_obj)
            {
                Ticks++;

                if (Ticks == Period)
                {
                    Ticks = 0;
                    pulse = true;

                    Tick();
                }
                else
                {
                    pulse = false;

                    if (did_pulse)
                    {
                        Tick();
                    }
                }
            }
        }

        public SteppingClock(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public SteppingClock(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public SteppingClock(Engine.Engine Engine) : this(null, Engine)
        {
        }
    }


    [Chip("RealTimeClock")]
    public class RealTimeClock : Clock
    {
        private System.TimeSpan period;

        [ChipProperty]
        public System.TimeSpan Period
        {
            get
            {
                return period;
            }
            set
            {
                period = value;

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private object last_pulse_lock_obj = new object();
        private System.DateTime last_pulse;

        private bool pulse;
        public override bool Pulse
        {
            get => pulse;
        }

        public override void Update()
        {
            lock (last_pulse_lock_obj) { 
                var now = System.DateTime.UtcNow;

                if (now - last_pulse >= Period)
                {
                    pulse = true;

                    last_pulse = now;

                    Tick();
                }
                else
                {
                    if (did_pulse)
                    {
                        pulse = false;
                    }

                    pulse = false;
                }
            }
        }

        public RealTimeClock(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            last_pulse = System.DateTime.UtcNow;
        }

        public RealTimeClock(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public RealTimeClock(Engine.Engine Engine) : this(null, Engine)
        {
        }

    }
}
