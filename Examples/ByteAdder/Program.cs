using System;

using CircuitSim2.Chips.IO.BasicInputs;
using CircuitSim2.Chips.Components.Adders;

namespace ByteAdderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new GenericInput<byte>();
            var b = new GenericInput<byte>();

            var cin = new Constant<bool>(false);

            var adder = new ByteAdder();

            adder.Inputs.A.Attach(a.Outputs.Out);
            adder.Inputs.B.Attach(b.Outputs.Out);
            adder.Inputs.Cin.Attach(cin.Outputs.Out);

            string line;

            while ((line = Console.ReadLine()) != null)
            {
                try
                {
                    var toks = line.Split(' ');

                    if (toks.Length != 2) throw new Exception("Invalid input");

                    a.Value = byte.Parse(toks[0].Trim());
                    b.Value = byte.Parse(toks[1].Trim());

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