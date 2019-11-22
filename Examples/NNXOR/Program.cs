﻿using System;
using System.Linq;

using CircuitSim2.Engine;
using CircuitSim2.Chips.Digital.Conversion;
using CircuitSim2.Chips.Digital.Logic;
using CircuitSim2.Chips.Neural.Networks;

namespace NNXOR
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (Engine Engine = new Engine())
            using (Engine Engine = null)
            {
                var A = new CircuitSim2.Chips.Digital.Inputs.Switch(Engine);
                var B = new CircuitSim2.Chips.Digital.Inputs.Switch(Engine);

                var convA = new ToDouble(Engine)
                {
                    Low = 0.0,
                    High = 1.0,
                };

                var convB = new ToDouble(Engine)
                {
                    Low = 0.0,
                    High = 1.0,
                };

                var XOR = new XOR(Engine);
                var convX = new CircuitSim2.Chips.Digital.Conversion.ToDouble(Engine)
                {
                    Low = 0.0,
                    High = 1.0,
                };

                var NN = new FeedForward(Engine)
                {
                    NumInputs = 2,
                    Layers = new int[] { 3, 1 },
                };

                XOR.Inputs.A.Attach(A.Outputs.Out);
                XOR.Inputs.B.Attach(B.Outputs.Out);

                convA.Inputs.A.Attach(A.Outputs.Out);
                convB.Inputs.A.Attach(B.Outputs.Out);

                convX.Inputs.A.Attach(XOR.Outputs.Out);

                NN.Inputs[0].Attach(convA.Outputs.Out);
                NN.Inputs[1].Attach(convB.Outputs.Out);

                var vals = new bool[] { false, true };

                var learningRate = 0.1;

                if (Engine != null)
                {
#if DEBUG
                    Engine.ChipSkipped += (s, e) => Console.WriteLine($"chip {e.Chip.ID} skipped");
                    Engine.ChipUpdated += (s, e) => Console.WriteLine($"chip {e.Chip.ID} updated");
#endif
                    Engine.Start(0.01);
                }

                foreach (var epoch in Enumerable.Range(0, 10000))
                {
                    foreach(var _ in Enumerable.Range(0, 2))
                    {
                        A.Toggle();

                        foreach (var __ in Enumerable.Range(0, 2))
                        {
                            B.Toggle();

                            Engine?.FlushAll();

                            NN.BackPropagate(new double[] { convX.Outputs.Out.Value }, learningRate);
                        }
                    }
                }

                /*foreach (var a in vals)
                {
                    foreach (var b in vals)
                    {
                        A.Value = a;
                        B.Value = b;*/

                foreach(var _ in Enumerable.Range(0, 2))
                {
                    foreach(var __ in Enumerable.Range(0, 2))
                    {

                        Engine?.FlushAll();

                        Console.WriteLine($"XOR({A.Outputs.Out.Value},{B.Outputs.Out.Value}) = {{ expected: {convX.Outputs.Out.Value}, actual = {NN.Outputs[0].Value} }}");
                    }
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
