using System;

namespace CircuitSim2.Chips.Functors
{
    public abstract class Generator<T> : ChipBase where T : IEquatable<T>
    {
        public readonly CircuitSim2.IO.GenericOutput<T> Outputs;

        public Generator(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
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

        public override SizeVec size => new SizeVec
        {
            Length = 1,
            Width = 2,
            Height = 1,
        };
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

        public sealed override void Output() => Outputs.Out.Value = _out;

        public sealed override void Tick()
        {
            base.Tick();
        }

        public override SizeVec size => new SizeVec
        {
            Length = 2.0,
            Width = 1.0,
            Height = 1.0,
        };
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

        public sealed override void Output() => Outputs.Out.Value = _out;

        public sealed override void Tick()
        {
            base.Tick();
        }

        public override SizeVec size => new SizeVec
        {
            Length = 1.0,
            Width = 3.0,
            Height = 1.0,
        };
    }
}