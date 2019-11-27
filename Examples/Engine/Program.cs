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
                var A = new CircuitSim2.Chips.Byte.Generators.Random(engine)
                {
                    Seed = 1234,
                };
                Console.WriteLine($"A.ID = {A.ID}");
                A.Outputs.Out.ValueChanged += (s, e) => Console.WriteLine($"A <- {A.Outputs.Out.Value}");

                var B = new CircuitSim2.Chips.Byte.Generators.Random(engine)
                {
                    Seed = 4321,
                };
                Console.WriteLine($"B.ID = {B.ID}");
                B.Outputs.Out.ValueChanged += (s, e) => Console.WriteLine($"B <- {B.Outputs.Out.Value}");

                var adder = new CircuitSim2.Chips.Byte.Arithmetic.Add(engine);
                Console.WriteLine($"adder.ID = {adder.ID}");
                adder.Outputs.Out.ValueChanged += (s, e) => Console.WriteLine($"A + B = {adder.Outputs.Out.Value}");

                adder.Inputs.A.Attach(A.Outputs.Out);
                adder.Inputs.B.Attach(B.Outputs.Out);

                if (engine != null)
                {
                    engine.ChipUpdated += (s, e) => Console.WriteLine($"{e.Chip.ID} updated");
                    engine.ChipSkipped += (s, e) => Console.WriteLine($"{e.Chip.ID} skipped");

                    engine.Start(1);
                }

                for (int i = 0; i < 10; i++)
                {
                    A.Tick();
                    B.Tick();

                    if(engine != null)
                        Thread.Sleep(100);
                }
            }

            Console.ReadLine();
        }
    }
}
