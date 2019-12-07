using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Timers;
using CircuitSim2.IO;

namespace CircuitSim2.Chips.Signals
{
    public abstract class EdgeDetector<T> : ChipBase
    {
        [NonSerialized]
        public readonly GenericInput<T> Inputs;
        [NonSerialized]
        public readonly GenericOutput<bool> Outputs;

        protected abstract bool Detector(T A, T B);

        protected EdgeDetector()
        {
            InputSet = (Inputs = new GenericInput<T>(this));
            OutputSet = (Outputs = new GenericOutput<bool>(this));

            _last = default;
        }

        [NonSerialized]
        private T _last;
        [NonSerialized]
        private bool _out;

        public sealed override void Compute()
        {
            _out = Detector(_last, Inputs.A.Value);
            _last = Inputs.A.Value;
        }

        public sealed override void Commit() => Outputs.Out.Value = _out;

        public override SizeVec size => new SizeVec
        {
            Length = 1.0,
            Width = 2.0,
            Height = 1.0,
        };
    }

    public abstract class Lag<T> : ChipBase
    {
        [NonSerialized]
        public readonly GenericInput<T> Inputs;
        [NonSerialized]
        public readonly GenericOutput<T> Outputs;

        [NonSerialized]
        private readonly System.Timers.Timer Timer;

        private long delay_ticks = 1000 * 10000;
        [ChipProperty]
        public long Delay_Ticks
        {
            get
            {
                return delay_ticks;
            }
            set
            {
                delay = new System.TimeSpan(value);
                delay_ticks = value;
            }
        }

        private struct timestamped_value
        {
            public System.DateTime Timestamp;
            public T Value;
        }

        [NonSerialized]
        private readonly ConcurrentQueue<timestamped_value> value_queue;

        [NonSerialized]
        private timestamped_value next_value;

        [NonSerialized]
        private bool have_next_value = false;

        private void elapsed(object sender, ElapsedEventArgs e)
        {
            if (!have_next_value)
            {
                if (value_queue.IsEmpty)
                {
                    return;
                }

                while (!value_queue.TryDequeue(out next_value))
                {
                    Thread.Sleep(0);
                }

                have_next_value = true;
            }


            if (have_next_value)
            {
                if (System.DateTime.UtcNow >= next_value.Timestamp)
                {
                    Outputs.Out.Value = next_value.Value;

                    have_next_value = false;
                }
            }
        }

        public sealed override void Compute()
        {
            value_queue.Enqueue(new timestamped_value
            {
                Timestamp = System.DateTime.UtcNow + new System.TimeSpan(delay_ticks),
                Value = Inputs.A.Value
            });
        }

        [NonSerialized]
        private System.TimeSpan delay;
        [ChipProperty]
        public System.TimeSpan Delay
        {
            get
            {
                return delay;
            }
            set
            {
                delay = value;
                delay_ticks = value.Ticks;
            }
        }

        protected Lag()
        {
            InputSet = (Inputs = new GenericInput<T>(this));
            OutputSet = (Outputs = new GenericOutput<T>(this));

            value_queue = new ConcurrentQueue<timestamped_value>();

            Timer = new System.Timers.Timer(1)
            {
                AutoReset = true,
                Enabled = true,
            };
            Timer.Elapsed += elapsed;
        }

        protected override void Dispose(bool disposing)
        {
            Timer?.Dispose();

            base.Dispose(disposing);
        }

    }
}
