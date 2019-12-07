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

        protected Clock()
        {
            InputSet = new NoInputs();

            OutputSet = (Outputs = new GenericOutput<bool>(this));
        }

        public abstract void Update();

        protected bool did_pulse
        {
            get; private set;
        } = false;

        public abstract bool Pulse { get; }
        
        [NonSerialized]
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
    [Serializable]
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

        [NonSerialized]
        private object ticks_lock_obj = new object();
        [NonSerialized]
        private ulong Ticks = 0;
        [NonSerialized]
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
    }


    [Chip("RealTimeClock")]
    [Serializable]
    public class RealTimeClock : Clock
    {
        private long period_ticks;
        
        [NonSerialized]
        private System.TimeSpan period;

        [ChipProperty]
        public long Period_Ticks
        {
            get
            {
                return period_ticks;
            }
            set
            {
                period = new System.TimeSpan(value);
                period_ticks = value;

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

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
                period_ticks = value.Ticks;

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        [NonSerialized]
        private object last_pulse_lock_obj = new object();
        [NonSerialized]
        private System.DateTime last_pulse;
        
        [NonSerialized]
        private bool pulse;
        public override bool Pulse
        {
            get => pulse;
        }

        public override void Update()
        {
            lock (last_pulse_lock_obj)
            {
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
    }
}
