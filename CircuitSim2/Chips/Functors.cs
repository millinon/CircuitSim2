using System;

namespace CircuitSim2.Chips.Functors
{
    public abstract class Generator<T> : ChipBase where T : IEquatable<T>
    {
        public readonly CircuitSim2.IO.ClockInputOnly Inputs;

        public readonly CircuitSim2.IO.GenericOutput<T> Outputs;

        public Generator(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = (Inputs = new CircuitSim2.IO.ClockInputOnly(this));

            OutputSet = (Outputs = new CircuitSim2.IO.GenericOutput<T>(this));
        }

        protected abstract T NextValue();

        private T _out;

        public sealed override void Compute() => _out = NextValue();

        public sealed override void Commit() => Outputs.Out.Value = _out;

        public sealed override void Tick()
        {
            base.Tick();
        }

        /*public override SizeVec size => new SizeVec
        {
            Length = 1,
            Width = 2,
            Height = 1,
        };*/
    }

    public abstract class Random<T> : Generator<T> where T : IEquatable<T>
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

        public Random(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            RNG = new Random(seed);
        }
    }

    [PureChip]
    public abstract class UnaryFunctor<T, U> : ChipBase where T : IEquatable<T> where U : IEquatable<U>
    {
        public readonly CircuitSim2.IO.GenericInput<T> Inputs;
        public readonly CircuitSim2.IO.GenericOutput<U> Outputs;

        public abstract U Func(T Value);

        public UnaryFunctor(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = (Inputs = new CircuitSim2.IO.GenericInput<T>(this));
            OutputSet = (Outputs = new CircuitSim2.IO.GenericOutput<U>(this));
        }

        private U _out;

        public sealed override void Compute() => _out = Func(Inputs.A.Value);

        public sealed override void Commit() => Outputs.Out.Value = _out;

        public sealed override void Tick() => base.Tick();

        /*public override SizeVec size => new SizeVec
        {
            Length = 2.0,
            Width = 1.0,
            Height = 1.0,
        };*/
    }

    [PureChip]
    public abstract class BinaryFunctor<T, U, V> : ChipBase where T : IEquatable<T> where U : IEquatable<U> where V : IEquatable<V>
    {
        public readonly CircuitSim2.IO.GenericInput<T, U> Inputs;
        public readonly CircuitSim2.IO.GenericOutput<V> Outputs;

        public abstract V Func(T Val1, U Val2);

        public BinaryFunctor(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = (Inputs = new CircuitSim2.IO.GenericInput<T, U>(this));
            OutputSet = (Outputs = new CircuitSim2.IO.GenericOutput<V>(this));
        }

        private V _out;

        public sealed override void Compute() => _out = Func(Inputs.A.Value, Inputs.B.Value);

        public sealed override void Commit() => Outputs.Out.Value = _out;

        public sealed override void Tick() => base.Tick();
    }

    public abstract class Constant<T> : ChipBase where T : IEquatable<T>
    {
        public readonly CircuitSim2.IO.NoInputs Inputs;
        public readonly CircuitSim2.IO.GenericOutput<T> Outputs;

        private T value;

        [ChipProperty]
        public T Value
        {
            get => value;
            set
            {
                this.value = Value;
                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        public Constant(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = (Inputs = new CircuitSim2.IO.NoInputs());
            OutputSet = (Outputs = new CircuitSim2.IO.GenericOutput<T>(this));
        }

        private T _out;
        public sealed override void Compute() => _out = Value;
        public sealed override void Commit() => Outputs.Out.Value = _out;
        public sealed override void Tick() => base.Tick();
    }
}