using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CircuitSim2.IO;

namespace CircuitSim2.Chips.Neural.Networks
{
    [Chip("FeedForward")]
    public sealed class FeedForward : ChipBase
    {
        public InputArray<double> Inputs
        {
            get; private set;
        }

        public OutputArray<double> Outputs
        {
            get; private set;
        }

        private Neuron[][] Neurons;

        //public readonly int[] Layers;


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
                numInputs = value;

                CreateNeurons();

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private int[] layers = new int[] { 1 };

        [ChipProperty]
        public int[] Layers
        {
            get
            {
                return layers;
            } set
            {
                layers = value;

                CreateNeurons();

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private void CreateNeurons()
        {
            Detach();

            if(Neurons != null)
            {
                for(int i = 0; i < Neurons.Length; i++)
                {
                    for(int j = 0; j < Neurons[i].Length; j++)
                    {
                        RemoveSubChip(Neurons[i][j]);
                        Neurons[i][j].Dispose();
                    }
                }
            }

            if (Layers == null) throw new ArgumentNullException("Layers");
            else if (Layers.Length < 1) throw new ArgumentException("Layers.Length must be >= 1");
            else if (Layers.Where(layer => layer <= 0).Any()) throw new ArgumentException("Empty layer detected");

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
                        AddSubChip(n = (Neurons[layer][neuron] = new Neuron(this)
                        {
                            RNG = Random,
                            NumInputs = NumInputs,
                        }));

                        for (int input = 0; input < NumInputs; input++)
                        {
                            n.Inputs[input].Bind(Inputs[input]);
                        }

                    }
                    else
                    {
                        AddSubChip(n = (Neurons[layer][neuron] = new Neuron(this)
                        {
                            RNG = Random,
                            NumInputs = Layers[layer-1],
                        }));

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

            Reset();
        }

        public FeedForward(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public FeedForward(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public FeedForward(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            CreateNeurons();
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

        public override SizeVec size => new SizeVec
        {
            Length = Layers.Max()+1,
            Width = Layers.Length+1,
            Height = 1,
        };
    }
}
