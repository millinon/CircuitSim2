using System;
using System.Threading;

using CircuitSim2.Chips.Components.Memory;

namespace DLatchTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CircuitSim2.Engine.Engine Engine = new CircuitSim2.Engine.Engine())
            {
                /*var E = new GenericInput<bool>(Engine);
                var D = new GenericInput<bool>(Engine); */
                var E = new CircuitSim2.Chips.Digital.Generators.Constant(Engine);
                var D = new CircuitSim2.Chips.Digital.Generators.Constant(Engine);

                var latch = new DLatch(Engine);

                latch.Inputs.D.Attach(D.Outputs.Out);
                latch.Inputs.E.Attach(E.Outputs.Out);

                Engine.Start(0.001);

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