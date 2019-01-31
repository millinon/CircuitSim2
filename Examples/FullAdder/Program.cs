using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CircuitSim2;
using CircuitSim2.Chips.Components.Adders;
using CircuitSim2.Chips.IO.BasicInputs;

namespace FullAdderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var A = new GenericInput<bool>();
            var B = new GenericInput<bool>();
            var Cin = new GenericInput<bool>();

            var Adder = new FullAdder();

            Adder.Inputs.A.Attach(A.Outputs.Out);
            Adder.Inputs.B.Attach(B.Outputs.Out);
            Adder.Inputs.Cin.Attach(Cin.Outputs.Out);

            var vals = new bool[] { false, true };

            foreach(var valA in vals)
            {
                A.Value = valA;

                foreach(var valB in vals)
                {
                    B.Value = valB;

                    foreach(var valC in vals)
                    {
                        Cin.Value = valC;

                        Console.WriteLine($"(A={valA} B={valB} Cin={valC}) -> (S={Adder.Outputs.S.Value} Cout={Adder.Outputs.Cout.Value})");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
