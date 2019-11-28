using System;

namespace CircuitSim2.Chips.Functors
{
    public abstract class Generator<T> : ChipBase
    {
        [NonSerialized]
        public readonly CircuitSim2.IO.ClockInputOnly Inputs;

        [NonSerialized]
        public readonly CircuitSim2.IO.GenericOutput<T> Outputs;

        public Generator()
        {
            InputSet = (Inputs = new CircuitSim2.IO.ClockInputOnly(this));
            OutputSet = (Outputs = new CircuitSim2.IO.GenericOutput<T>(this));
        }

        protected abstract T NextValue();

        [NonSerialized]
        private T _out;

        public sealed override void Compute() => _out = NextValue();

        public sealed override void Commit() => Outputs.Out.Value = _out;

        public sealed override void Tick()
        {
            if (Inputs.Clk.Trigger)
            {
                base.Tick();
            }
        }
    }

    public abstract class Random<T> : Generator<T>
    {
        protected Random RNG;

        private int seed;

        [ChipProperty]
        public int Seed
        {
            get
            {
                return seed;
            }
            set
            {
                seed = value;
                RNG = new Random(seed);

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        public Random()
        {
            RNG = new Random(seed);
        }
    }

    [PureChip]
    public abstract class UnaryFunctor<T, U> : ChipBase
    {
        [NonSerialized]
        public readonly CircuitSim2.IO.GenericInput<T> Inputs;
        [NonSerialized]
        public readonly CircuitSim2.IO.GenericOutput<U> Outputs;

        public abstract U Func(T Value);

        public UnaryFunctor()
        {
            InputSet = (Inputs = new CircuitSim2.IO.GenericInput<T>(this));
            OutputSet = (Outputs = new CircuitSim2.IO.GenericOutput<U>(this));
        }

        [NonSerialized]
        private U _out;

        public sealed override void Compute() => _out = Func(Inputs.A.Value);

        public sealed override void Commit() => Outputs.Out.Value = _out;

        public sealed override void Tick() => base.Tick();
    }

    [PureChip]
    public abstract class BinaryFunctor<T, U, V> : ChipBase
    {
        public readonly CircuitSim2.IO.GenericInput<T, U> Inputs;
        public readonly CircuitSim2.IO.GenericOutput<V> Outputs;

        public abstract V Func(T Val1, U Val2);

        public BinaryFunctor()
        {
            InputSet = (Inputs = new CircuitSim2.IO.GenericInput<T, U>(this));
            OutputSet = (Outputs = new CircuitSim2.IO.GenericOutput<V>(this));
        }

        [NonSerialized]
        private V _out;

        public sealed override void Compute() => _out = Func(Inputs.A.Value, Inputs.B.Value);

        public sealed override void Commit() => Outputs.Out.Value = _out;

        public sealed override void Tick() => base.Tick();
    }

    public class Constant<T> : ChipBase
    {
        public readonly CircuitSim2.IO.GenericOutput<T> Outputs;

        private T value;

        [ChipProperty]
        public T Value
        {
            get => value;
            set
            {
                this.value = value;

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        public Constant() : base()
        {
            InputSet = new CircuitSim2.IO.NoInputs();
            OutputSet = (Outputs = new CircuitSim2.IO.GenericOutput<T>(this));
        }

        [NonSerialized]
        private T _out;
        public sealed override void Compute() => _out = Value;
        public sealed override void Commit() => Outputs.Out.Value = _out;
        public sealed override void Tick() => base.Tick();
    }

    public class Clamp<T> : ChipBase where T:IComparable<T>
    {
        private T minvalue;
        [ChipProperty]
        public T MinValue
        {
            get => minvalue;
            set
            {
                minvalue = value;

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private T maxvalue;
        [ChipProperty]
        public T MaxValue
        {
            get => maxvalue;
            set
            {
                maxvalue = value;

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        [NonSerialized]
        public readonly CircuitSim2.IO.GenericInput<T> Inputs;

        [NonSerialized]
        public readonly CircuitSim2.IO.GenericOutput<T> Outputs;

        public Clamp()
        {
            InputSet = (Inputs = new CircuitSim2.IO.GenericInput<T>(this));
            OutputSet = (Outputs = new CircuitSim2.IO.GenericOutput<T>(this));
        }

        [NonSerialized]
        private T value;
        public sealed override void Compute()
        {
            value = Inputs.A.Value;
        
            if(value.CompareTo(MinValue) < 0)
            {
                value = MinValue;
            } else if(value.CompareTo(MaxValue) > 0)
            {
                value = MaxValue;
            }
        }

        public sealed override void Commit()
        {
            Outputs.Out.Value = value;
        }
    }
}