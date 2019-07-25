using System;
using System.Linq;

using CircuitSim2.IO;

namespace CircuitSim2.Chips.Neural
{
    [Chip("Neuron")]
    [PureChip]
    public sealed class Neuron : ChipBase
    {
        public readonly InputArray<double> Inputs;
        public readonly GenericOutput<double> Outputs;

        public readonly int NumInputs;

        public readonly double[] Weights;
        public double Bias;

        public readonly Func<double, double> Phi;

        public Neuron(int NumInputs, Random Random, Engine.Engine Engine = null) : this(NumInputs, a => 1.0 / (1.0 + Math.Exp(-a)), Random, Engine)
        {

        }

        public Neuron(int NumInputs, Func<double, double> Phi, Random Random, Engine.Engine Engine = null) : base(Engine)
        {
            if (NumInputs <= 0) throw new ArgumentException("Neuron must have a positive number of inputs");
            else if (Phi == null) throw new ArgumentNullException("Phi");

            InputSet = (Inputs = new InputArray<double>(this, NumInputs));
            OutputSet = (Outputs = new GenericOutput<double>(this));

            Weights = new double[NumInputs];
            for (int i = 0; i < NumInputs; i++) Weights[i] = Random.NextDouble();

            this.Phi = Phi;

            Bias = 0.0;

            this.NumInputs = NumInputs;
        }

        private double _out;

        public override void Compute()
        {
            var sum = Enumerable.Range(0, NumInputs).Select(i => Inputs[i].Value * Weights[i]).Sum() + Bias;

            _out = Phi(sum);
        }

        public override void Output()
        {
            Outputs.Out.Value = _out;
        }
    }
}
