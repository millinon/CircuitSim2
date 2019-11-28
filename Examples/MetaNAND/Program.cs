using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CircuitSim2.Chips;
using CircuitSim2.IO;
//using CircuitSim2.Chips.IO.BasicInputs;
using CircuitSim2.Chips.Digital.Logic;
using static CircuitSim2.Chips.MetaChip.MetaChipDescription;
using static CircuitSim2.Chips.MetaChip.MetaChipDescription.ChipDescription;

namespace MetaNANDTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var A = new CircuitSim2.Chips.IO.BasicInputs.GenericInput<bool>();
            //var B = new CircuitSim2.Chips.IO.BasicInputs.GenericInput<bool>();

            var A = new CircuitSim2.Chips.Digital.Generators.Constant();
            var B = new CircuitSim2.Chips.Digital.Generators.Constant();

            A.Value = false;
            B.Value = false;

            var NAND = new NAND();
            NAND.Inputs.A.Attach(A.Outputs.Out);
            NAND.Inputs.B.Attach(B.Outputs.Out);

            MetaChip MetaNAND = new MetaChip();

            var bool_type = new MetaChip.TypeDescription
            {
                AssemblyFullName = typeof(bool).Assembly.FullName,
                Namespace = typeof(bool).Namespace,
                Name = typeof(bool).Name,
            };

            var and_type = new MetaChip.TypeDescription
            {
                AssemblyFullName = typeof(AND).Assembly.FullName,
                Namespace = typeof(AND).Namespace,
                Name = typeof(AND).Name,
            };

            var not_type = new MetaChip.TypeDescription
            {
                AssemblyFullName = typeof(NOT).Assembly.FullName,
                Namespace = typeof(NOT).Namespace,
                Name = typeof(NOT).Name,
            };

            MetaNAND.Load(new MetaChip.MetaChipDescription
            {
                Inputs = new List<InputDescription>
                {
                    new InputDescription
                    {
                        Name = "A",
                        Type = bool_type,
                    },
                    new InputDescription
                    {
                        Name = "B",
                        Type = bool_type,
                    }
                },
                Outputs = new List<OutputDescription>
                {
                    new OutputDescription
                    {
                        Name = "Out",
                        Type =  bool_type,
                        MapID = "NOT",
                        MapOutput = "Out",
                    }
                },
                Chips = new List<ChipDescription>
                {
                    new ChipDescription
                    {
                        Type = and_type,
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
                        Type = not_type,
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

            foreach (var valA in vals)
            {
                A.Value = valA;

                foreach (var valB in vals)
                {
                    B.Value = valB;

                    Console.Write($"!({valA} & {valB}) => {NAND.Outputs.Out.Value}: ");

                    if ((MetaNAND.OutputSet["Out"] as Output<bool>).Value == NAND.Outputs.Out.Value)
                    {
                        Console.WriteLine("PASS");
                    }
                    else
                    {
                        Console.WriteLine($"FAIL: {(MetaNAND.OutputSet["Out"] as Output<bool>).Value}");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
