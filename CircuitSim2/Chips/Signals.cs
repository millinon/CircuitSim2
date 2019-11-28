using System;

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

        public EdgeDetector()
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
}
