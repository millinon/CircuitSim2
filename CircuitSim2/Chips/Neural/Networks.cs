using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CircuitSim2.IO;

namespace CircuitSim2.Chips.Neural.Networks
{
    [Chip("FeedForward")]
    [PureChip]
    public sealed class FeedForward : ChipBase
    {
        public readonly InputArray<double> Inputs;
        public readonly OutputArray<double> Outputs;

        private readonly Neuron[][] Neurons;

        public readonly int[] Layers;

        public FeedForward(int NumInputs, int[] Layers) : this(NumInputs, Layers, null, null)
        {
        }

        public FeedForward(int NumInputs, int[] Layers, ChipBase ParentChip) : this(NumInputs, Layers, ParentChip, ParentChip?.Engine)
        {
        }

        public FeedForward(int NumInputs, int[] Layers, Engine.Engine Engine) : this(NumInputs, Layers, null, Engine)
        {
        }

        private FeedForward(int NumInputs, int[] Layers, ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            if (Layers == null) throw new ArgumentNullException("Layers");
            else if (Layers.Length < 1) throw new ArgumentException("Layers.Length must be >= 1");
            else if (Layers.Where(layer => layer <= 0).Any()) throw new ArgumentException("Empty layer detected");

            this.Layers = Layers;

            InputSet = (Inputs = new InputArray<double>(this, NumInputs));
            OutputSet = (Outputs = new OutputArray<double>(this, Layers[Layers.Length - 1]));

            var Random = new Random();

            Neurons = new Neuron[Layers.Length][];

            for (int layer = 0; layer < Layers.Length; layer++)
            {
                Neurons[layer] = new Neuron[Layers[layer]];

                for (int neuron = 0; neuron < Layers[layer]; neuron++)
                {
                    Neuron n;

                    if (layer == 0)
                    {
                        n = (Neurons[layer][neuron] = new Neuron(NumInputs, Random, Engine));

                        for (int input = 0; input < NumInputs; input++)
                        {
                            n.Inputs[input].Bind(Inputs[input]);
                        }

                    } else
                    {
                        n = (Neurons[layer][neuron] = new Neuron(Layers[layer - 1], Random, Engine));

                        for (int input = 0; input < Layers[layer - 1]; input++)
                        {
                            n.Inputs[input].Attach(Neurons[layer - 1][input].Outputs.Out);
                        }

                        if (layer == Layers.Length - 1)
                        {
                            Outputs[neuron].Bind(n.Outputs.Out);
                        }
                    }
                }
            }
        }

        public void BackPropagate(double[] Expected, double LearningRate)
        {
            if (Expected == null) throw new ArgumentNullException(nameof(Expected));
            else if (Expected.Length != Outputs.Length) throw new ArgumentException("Expected[] length != NumOutputs");

            double[][] gradients = new double[Layers.Length][];
            for(int layer = 0; layer < Layers.Length; layer++)
            {
                gradients[layer] = new double[Layers[layer]];
            }

            double gradient;

            for(var output = 0; output < Outputs.Length; output++)
            {
                var neuron = Neurons[Layers.Length - 1][output];
                var value = neuron.Outputs.Out.Value;

                gradients[Layers.Length - 1][output] = (value - Expected[output]) * value * (1.0 - value);
            }

            for(var layer = Layers.Length - 2; layer >= 0; layer--)
            {
                for(var neuron_idx = 0; neuron_idx < Layers[layer]; neuron_idx++)
                {
                    var neuron = Neurons[layer][neuron_idx];
                    var value = neuron.Outputs.Out.Value;

                    gradient = 0.0;

                    for(int output = 0; output < Layers[layer+1]; output++)
                    {
                        gradient += gradients[layer + 1][output] * Neurons[layer + 1][output].Weights[neuron_idx];
                    }

                    gradients[layer][neuron_idx] = gradient * value * (1.0 - value);
                }
            }

            for(int layer = 0; layer < Layers.Length; layer++)
            {
                for(int neuron_idx = 0; neuron_idx < Layers[layer]; neuron_idx++)
                {
                    var neuron = Neurons[layer][neuron_idx];

                    for(var input = 0; input < neuron.Inputs.Length; input++)
                    {
                        neuron.Weights[input] -= LearningRate * neuron.Inputs[input].Value * gradients[layer][neuron_idx];
                    }
                }
            }
        }
    }
}
