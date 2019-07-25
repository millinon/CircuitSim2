using System;
using System.Threading;

using CircuitSim2.Chips.Components.Memory;

using CircuitSim2.Chips.IO.BasicInputs;

namespace DLatchTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CircuitSim2.Engine.Engine Engine = new CircuitSim2.Engine.Engine())
            {
                var E = new GenericInput<bool>(Engine);
                var D = new GenericInput<bool>(Engine);

                var latch = new DLatch(Engine);

                latch.Inputs.D.Attach(D.Outputs.Out);
                latch.Inputs.E.Attach(E.Outputs.Out);

                Engine.Start(1);

                string line;

                while ((line = Console.ReadLine()) != null)
                {
                    if (line == "0")
                    {
                        E.Value = true;
                        D.Value = false;
                        Thread.Sleep(300);
                        E.Value = false;
                    }
                    else if (line == "1")
                    {
                        E.Value = true;
                        D.Value = true;
                        Thread.Sleep(300);
                        E.Value = false;
                    }

                    Thread.Sleep(100);

                    Console.WriteLine($"Q <- {(latch.OutputSet["Q"] as CircuitSim2.IO.Output<bool>).Value}");
                }
            }
        }
    }
}