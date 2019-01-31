using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CircuitSim2;
using CircuitSim2.Chips.IO.BasicInputs;
using CircuitSim2.Chips.Digital.Logic;

namespace Dummy
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericInput<bool> A = new GenericInput<bool>();

            GenericInput<bool> B = new GenericInput<bool>();

            AND chip = new AND();

            chip.Inputs.A.Attach(A.Outputs.Out);
            chip.Inputs.B.Attach(B.Outputs.Out);

            var vals = new bool[] { false, true };

            foreach(var valA in vals)
            {
                A.Value = valA;

                foreach (var valB in vals)
                {
                    B.Value = valB;

                    Console.WriteLine($"({A.Value} & {B.Value}) == {chip.Outputs.Out.Value}");
                }
            }

            Console.ReadLine();
        }
    }
}
