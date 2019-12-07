using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lag
{
    class Program
    {
        static void Main(string[] args)
        {
            CircuitSim2.Chips.String.Generators.Constant input = new CircuitSim2.Chips.String.Generators.Constant();
            CircuitSim2.Chips.String.Signals.Lag lag = new CircuitSim2.Chips.String.Signals.Lag()
            {
                Delay = new TimeSpan(0, 0, 5),
            };

            lag.Inputs.A.Attach(input.Outputs.Out);
            lag.Outputs.Out.ValueChanged += (s, e) =>
            {
                Console.WriteLine(lag.Outputs.Out.Value);
            };

            string line;
            while((line = Console.ReadLine()) != null){
                input.Value = line;
            }




        }
    }
}
