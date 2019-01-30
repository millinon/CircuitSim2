using System;

namespace CircuitSim2.Chips.Functors
{
    public abstract class Generator<T> : ChipBase where T : IEquatable<T>
    {
        public readonly CircuitSim2.IO.GenericOutput<T> Outputs;

        public Generator(string Name, Engine.Engine Engine = null) : base(Name, Engine)
        {
            Outputs = new CircuitSim2.IO.GenericOutput<T>(this);

            InputSet = new CircuitSim2.IO.NoInputs();
            OutputSet = Outputs;
        }

        protected abstract T NextValue();

        private T _out;

        public sealed override void Compute() => _out = NextValue();

        public sealed override void Output() => Outputs.Out.Value = _out;
    }

    public abstract class UnaryFunctor<T, U> : ChipBase where T : IEquatable<T> where U : IEquatable<U>
        {
            public readonly CircuitSim2.IO.GenericInput<T> Inputs;
            public readonly CircuitSim2.IO.GenericOutput<U> Outputs;

            private readonly Func<T, U> Func;

            public UnaryFunctor(string Name, Func<T, U> Func, Engine.Engine Engine = null) : base(Name, Engine)
            {
                Inputs = new CircuitSim2.IO.GenericInput<T>(this);
                Outputs = new CircuitSim2.IO.GenericOutput<U>(this);

                InputSet = Inputs;
                OutputSet = Outputs;

                this.Func = Func;
            }

            private U _out;

        public sealed override void Compute() => _out = Func(Inputs.A.Value);

        public sealed override void Output() => Outputs.Out.Value = _out;
    }

    public abstract class BinaryFunctor<T, U, V> : ChipBase where T : IEquatable<T> where U : IEquatable<U> where V : IEquatable<V>
    {
        public readonly CircuitSim2.IO.GenericInput<T, U> Inputs;
        public readonly CircuitSim2.IO.GenericOutput<V> Outputs;

        private readonly Func<T, U, V> Func;

        public BinaryFunctor(string Name, Func<T, U, V> Func, Engine.Engine Engine = null) : base(Name, Engine)
        {
            Inputs = new CircuitSim2.IO.GenericInput<T, U>(this);
            Outputs = new CircuitSim2.IO.GenericOutput<V>(this);

            InputSet = Inputs;
            OutputSet = Outputs;

            this.Func = Func;
        }

        private V _out;

        public sealed override void Compute() => _out = Func(Inputs.A.Value, Inputs.B.Value);

        public sealed override void Output() => Outputs.Out.Value = _out;
    }
}