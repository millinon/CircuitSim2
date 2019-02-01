using System;
using System.Threading;

using CircuitSim2.Engine;

namespace EngineTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var engine = new Engine())
            {
                var A = new CircuitSim2.Chips.Byte.Generators.Random(1234, engine);
                Console.WriteLine($"A.ID = {A.ID}");
                A.Outputs.Out.ValueChanged += (s, e) => Console.WriteLine($"A <- {e.NewValue}");

                var B = new CircuitSim2.Chips.Byte.Generators.Random(4321, engine);
                Console.WriteLine($"B.ID = {B.ID}");
                B.Outputs.Out.ValueChanged += (s, e) => Console.WriteLine($"B <- {e.NewValue}");

                var adder = new CircuitSim2.Chips.Byte.Arithmetic.Add(engine);
                Console.WriteLine($"adder.ID = {adder.ID}");
                adder.Outputs.Out.ValueChanged += (s, e) => Console.WriteLine($"A + B = {e.NewValue}");

                adder.Inputs.A.Attach(A.Outputs.Out);
                adder.Inputs.B.Attach(B.Outputs.Out);

                engine.ChipUpdated += (s, e) => Console.WriteLine($"{e.Chip.ID} updated");
                engine.ChipSkipped += (s, e) => Console.WriteLine($"{e.Chip.ID} skipped");

                engine.Start(100);

                for (int i = 0; i < 10; i++)
                {
                    A.Tick();
                    B.Tick();

                    Thread.Sleep(1000);
                }
            }

            Console.ReadLine();
        }
    }
}
