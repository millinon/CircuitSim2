using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CircuitSim2.Chips;
using CircuitSim2.Chips.IO.BasicInputs;
using CircuitSim2.Chips.Digital.Logic;
using static CircuitSim2.Chips.MetaChip.MetaChipDescription;
using static CircuitSim2.Chips.MetaChip.MetaChipDescription.ChipDescription;

namespace MetaNAND
{
    class Program
    {
        static void Main(string[] args)
        {
            var A = new GenericInput<bool>();
            var B = new GenericInput<bool>();

            A.Value = false;
            B.Value = false;

            var NAND = new NAND();
            NAND.Inputs.A.Attach(A.Outputs.Out);
            NAND.Inputs.B.Attach(B.Outputs.Out);

            MetaChip MetaNAND = new MetaChip();
            MetaNAND.Load(new MetaChip.MetaChipDescription
            {
                Inputs = new List<InputDescription>
                {
                    new InputDescription
                    {
                        Name = "A",
                        Type = CircuitSim2.IO.Type.DIGITAL,
                    },
                    new InputDescription
                    {
                        Name = "B",
                        Type = CircuitSim2.IO.Type.DIGITAL,
                    }
                },
                Outputs = new List<OutputDescription>
                {
                    new OutputDescription
                    {
                        Name = "Out",
                        Type = CircuitSim2.IO.Type.DIGITAL,
                        MapID = "NOT",
                        MapOutput = "Out",
                    }
                },
                Chips = new List<ChipDescription>
                {
                    new ChipDescription
                    {
                        Name = "AND",
                        Namespace = "CircuitSim2.Chips.Digital.Logic",
                        ID = "AND",
                        AutoTick = true,
                        Bindings = new List<InputBindingDescription>
                        {
                            new InputBindingDescription
                            {
                                Name = "A",
                                BindName = "A",
                            },
                            new InputBindingDescription
                            {
                                Name = "B",
                                BindName = "B",
                            }
                        }
                    },
                    new ChipDescription
                    {
                        Name = "NOT",
                        Namespace = "CircuitSim2.Chips.Digital.Logic",
                        ID = "NOT",
                        AutoTick = true,
                    }
                },
                Connections = new List<ConnectionDescription>
                {
                    new ConnectionDescription
                    {
                        SrcID = "AND",
                        SrcOutput = "Out",
                        DestID = "NOT",
                        DestInput = "A",
                    }
                },
            });

            MetaNAND.InputSet["A"].Attach(A.Outputs.Out);
            MetaNAND.InputSet["B"].Attach(B.Outputs.Out);

            var vals = new bool[] { false, true };

            foreach(var valA in vals)
            {
                A.Value = valA;

                foreach(var valB in vals)
                {
                    B.Value = valB;

                    Console.Write($"!({valA} & {valB}) => {NAND.Outputs.Out.Value}: ");

                    if((MetaNAND.OutputSet.OutputLookup("Out") as CircuitSim2.IO.Output<bool>).Value == NAND.Outputs.Out.Value)
                    {
                        Console.WriteLine("PASS");
                    } else {
                        Console.WriteLine($"FAIL: {(MetaNAND.OutputSet.OutputLookup("Out") as CircuitSim2.IO.Output<bool>).Value}");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
