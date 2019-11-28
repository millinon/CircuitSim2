using CircuitSim2.Chips;
using CircuitSim2.Engine;
using System;
using System.Linq;

namespace IntList
{
    class Program
    {
        class LessThanHalfMax : CircuitSim2.Chips.Functors.UnaryFunctor<int, bool>
        {
            public override bool Func(int Value)
            {
                return Value < (int.MaxValue / 2);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"HalfMax = {int.MaxValue / 2}");
            Console.WriteLine();

            var clk = new CircuitSim2.Chips.Time.SteppingClock()
            {
                Period = 2,
            };

            var gen = new CircuitSim2.Chips.List.Generator<int>()
            {
                Count = 10,
                Function = typeof(CircuitSim2.Chips.Integer.Generators.Random),
            };

            var flt = new CircuitSim2.Chips.List.Filter<int>()
            {
                Predicate = typeof(LessThanHalfMax)
            };

            var rdc = new CircuitSim2.Chips.List.Fold<int, int>()
            {
                Function = typeof(CircuitSim2.Chips.Integer.Arithmetic.Add)
            };

            var map = new CircuitSim2.Chips.List.Map<int, string>()
            {
                Function = typeof(CircuitSim2.Chips.Integer.Conversion.ToString)
            };

            gen.Inputs.Clk.Attach(clk.Outputs.Out);
            flt.Inputs.A.Attach(gen.Outputs.Out);
            rdc.Inputs.A.Attach(flt.Outputs.Out);
            map.Inputs.A.Attach(flt.Outputs.Out);

            clk.Update();

            for (int iter = 0; iter < 5; iter++)
            {
                clk.Update();

                Console.WriteLine($"gen: [{string.Join(",", gen.Outputs.Out.Value.Select(i => i.ToString()))}]");
                Console.WriteLine($"flt: [{string.Join(",", flt.Outputs.Out.Value.Select(i => i.ToString()))}]");
                Console.WriteLine($"rdc: {rdc.Outputs.Out.Value}");
                Console.WriteLine($"map: [{string.Join(",", map.Outputs.Out.Value)}]");
                Console.WriteLine();

                clk.Update();
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
