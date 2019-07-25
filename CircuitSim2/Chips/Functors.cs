using System;

namespace CircuitSim2.Chips.Functors
{
    public abstract class Generator<T> : ChipBase where T : IEquatable<T>
    {
        public readonly CircuitSim2.IO.GenericOutput<T> Outputs;

        public Generator(Engine.Engine Engine) : base(Engine)
        {
            OutputSet = (Outputs = new CircuitSim2.IO.GenericOutput<T>(this));

            InputSet = new CircuitSim2.IO.NoInputs();
        }

        protected abstract T NextValue();

        private T _out;

        public sealed override void Compute() => _out = NextValue();

        public sealed override void Output() => Outputs.Out.Value = _out;

        public sealed override void Tick()
        {
            base.Tick();
        }
    }

    [PureChip]
    public abstract class UnaryFunctor<T, U> : ChipBase where T : IEquatable<T> where U : IEquatable<U>
    {
        public readonly CircuitSim2.IO.GenericInput<T> Inputs;
        public readonly CircuitSim2.IO.GenericOutput<U> Outputs;

        private readonly Func<T, U> Func;

        public UnaryFunctor(Func<T, U> Func, Engine.Engine Engine) : base(Engine)
        {
            InputSet = (Inputs = new CircuitSim2.IO.GenericInput<T>(this));
            OutputSet = (Outputs = new CircuitSim2.IO.GenericOutput<U>(this));

            this.Func = Func;
        }

        private U _out;

        public sealed override void Compute() => _out = Func(Inputs.A.Value);

        public sealed override void Output() => Outputs.Out.Value = _out;

        public sealed override void Tick()
        {
            base.Tick();
        }
    }

    [PureChip]
    public abstract class BinaryFunctor<T, U, V> : ChipBase where T : IEquatable<T> where U : IEquatable<U> where V : IEquatable<V>
    {
        public readonly CircuitSim2.IO.GenericInput<T, U> Inputs;
        public readonly CircuitSim2.IO.GenericOutput<V> Outputs;

        private readonly Func<T, U, V> Func;

        public BinaryFunctor(Func<T, U, V> Func, Engine.Engine Engine) : base(Engine)
        {
            InputSet = (Inputs = new CircuitSim2.IO.GenericInput<T, U>(this));
            OutputSet = (Outputs = new CircuitSim2.IO.GenericOutput<V>(this));

            this.Func = Func;
        }

        private V _out;

        public sealed override void Compute() => _out = Func(Inputs.A.Value, Inputs.B.Value);

        public sealed override void Output() => Outputs.Out.Value = _out;

        public sealed override void Tick()
        {
            base.Tick();
        }
    }
}