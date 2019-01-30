using System;

using CircuitSim2.IO;

namespace CircuitSim2.Chips.Signals
{
    public abstract class EdgeDetector<T> : ChipBase where T:IEquatable<T>
    {
        public readonly GenericInput<T> Inputs;
        public readonly GenericOutput<bool> Outputs;

        private readonly Func<T, T, bool> Detector;

        public EdgeDetector(string Name, Func<T, T, bool> Detector, Engine.Engine Engine = null) : base(Name, Engine)
        {
            Inputs = new GenericInput<T>(this);
            Outputs = new GenericOutput<bool>(this);

            this.Detector = Detector;

            InputSet = Inputs;
            OutputSet = Outputs;

            _last = default(T);
        }

        private T _last;
        private bool _out;

        public sealed override void Compute()
        {
            _out = Detector(_last, Inputs.A.Value);
            _last = Inputs.A.Value;
        }

        public sealed override void Output() => Outputs.Out.Value = _out;
    }
}
