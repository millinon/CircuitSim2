using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CircuitSim2.IO;

namespace CircuitSim2.Chips.Neural
{
    [Chip("Neuron")]
    [Serializable]
    public class Neuron : ChipBase
    {
        [Serializable]
        public class WeightCollection : IEnumerable<double>
        {
            private double[] values;

            public IEnumerator<double> GetEnumerator()
            {
                return ((IEnumerable<double>)values).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<double>)values).GetEnumerator();
            }

            public double this[int idx]
            {
                get
                {
                    return values[idx];
                }
                set
                {
                    values[idx] = value;

                    if (Chip != null)
                    {
                        Chip.Reset();

                        if (Chip.AutoTick)
                        {
                            Chip.Tick();
                        }
                    }
                }
            }


            [NonSerialized]
            private Neuron chip;
            public Neuron Chip
            {
                get
                {
                    return chip;
                } set
                {
                    this.chip = value;
                }
            }

            public WeightCollection(Neuron Chip, int Capacity, Random RNG)
            {
                values = Enumerable.Range(0, Capacity).Select(_ => RNG.NextDouble()).ToArray();
                this.Chip = Chip;
            }

            public WeightCollection(double[] values)
            {
                this.values = values;
            }

        }

        public InputArray<double> Inputs
        {
            get; private set;
        }

        public GenericOutput<double> Outputs
        {
            get; private set;
        }


        public WeightCollection Weights
        {
            get; private set;
        }

        private double bias = 0.0;
        [ChipProperty]
        public double Bias
        {
            get
            {
                return bias;
            }
            set
            {
                bias = value;

                Reset();

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private int numInputs = 1;
        [ChipProperty]
        public int NumInputs
        {
            get
            {
                return numInputs;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(nameof(NumInputs));
                }

                numInputs = value;

                CreateInputs();

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        [NonSerialized]
        private Random rng = new Random();
        public Random RNG
        {
            get
            {
                return rng;
            }
            set
            {
                rng = value;

                CreateInputs();

                if (AutoTick)
                {
                    Tick();
                }
            }
        }


        private void CreateInputs()
        {
            if (NumInputs <= 0) throw new ArgumentException("Neuron must have a positive number of inputs");

            InputSet = (Inputs = new InputArray<double>(this, NumInputs));
            OutputSet = (Outputs = new GenericOutput<double>(this));

            Weights = new WeightCollection(this, NumInputs, RNG);

            Bias = 0.0;

            Reset();
        }

        protected virtual double Phi(double A) => 1.0 / (1.0 + Math.Exp(-A));

        public Neuron()
        {
            CreateInputs();
        }

        [NonSerialized]
        private double _out;

        public sealed override void Compute()
        {
            var sum = Enumerable.Range(0, NumInputs).Select(i => Inputs[i].Value * Weights[i]).Sum() + Bias;

            _out = Phi(sum);
        }

        public override void Commit()
        {
            Outputs.Out.Value = _out;
        }

        public override SizeVec size => new SizeVec
        {
            Length = 1.0,
            Width = NumInputs + 1.0,
            Height = 1.0,
        };
    }
}
