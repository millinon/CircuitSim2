using System;

using CircuitSim2.Chips.Components.Adders;
using System.Threading;
using CircuitSim2.Chips.Functors;

namespace ByteAdderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CircuitSim2.Engine.Engine engine = new CircuitSim2.Engine.Engine())
            {
                /*                var a = new GenericInput<byte>(engine);
                                var b = new GenericInput<byte>(engine);
                                var cin = new Constant<bool>(engine);
                                */

                var a = new CircuitSim2.Chips.Byte.Generators.Constant(engine);
                var b = new CircuitSim2.Chips.Byte.Generators.Constant(engine);
                var cin = new CircuitSim2.Chips.Digital.Generators.Constant(engine);
               
                var adder = new ByteAdder(engine);
                Console.WriteLine($"adder = {adder.ID}");

                adder.Inputs.A.Attach(a.Outputs.Out);
                adder.Inputs.B.Attach(b.Outputs.Out);
                adder.Inputs.Cin.Attach(cin.Outputs.Out);

                engine.ChipUpdated += (s, e) => Console.WriteLine($"Chip {e.Chip.ID} updated");
                engine.ChipSkipped += (s, e) => Console.WriteLine($"Chip {e.Chip.ID} skipped");

                if (engine != null)
                {
                    engine.Start(1);
                }

                string line;

                while ((line = Console.ReadLine()) != null)
                {
                    try
                    {
                        var toks = line.Split(' ');

                        if (toks.Length != 2) throw new Exception("Invalid input");

                        a.Value = byte.Parse(toks[0].Trim());
                        b.Value = byte.Parse(toks[1].Trim());

                        if (engine != null)
                            Thread.Sleep(5000);

                        if (adder.Outputs.Cout.Value) throw new Exception("Overflow");
                        Console.WriteLine($"{a.Value.ToString("X2")} + {b.Value.ToString("X2")} = {adder.Outputs.S.Value.ToString("X2")}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}