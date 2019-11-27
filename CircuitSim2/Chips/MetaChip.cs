using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace CircuitSim2.Chips
{
    [Chip("MetaChip")]
    public sealed class MetaChip : ChipBase
    {
        [Serializable]
        public sealed class TypeDescription
        {
            public string AssemblyFullName;
            public string Namespace;
            public string Name;
        }

        [Serializable]
        public sealed class MetaChipDescription
        {
            [Serializable]
            public sealed class InputDescription
            {
                public string Name;
                public TypeDescription Type;
            }
            public List<InputDescription> Inputs;

            [Serializable]
            public sealed class OutputDescription
            {
                public string Name;
                public TypeDescription Type;
                public string MapID;
                public string MapOutput;
            }
            public List<OutputDescription> Outputs;

            [Serializable]
            public sealed class ChipDescription
            {
                [Serializable]
                public sealed class InputBindingDescription
                {
                    public string Name;
                    public string BindName;
                }

                public TypeDescription Type;

                public string ID;

                public bool AutoTick;
                public PositionVec Position;
                public RotationVec Rotation;
                public double Scale;

                public List<InputBindingDescription> Bindings;
            }
            public List<ChipDescription> Chips;

            [Serializable]
            public sealed class ConnectionDescription
            {
                public string SrcID;
                public string SrcOutput;
                public string DestID;
                public string DestInput;
            }
            public List<ConnectionDescription> Connections;
        }

        [ChipProperty]
        public List<Assembly> AssemblySearchPath;

        public MetaChip(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = new CircuitSim2.IO.NoInputs();
            OutputSet = new CircuitSim2.IO.NoOutputs();

            AssemblySearchPath = new List<Assembly>()
            {
                typeof(object).Assembly,
                typeof(bool).Assembly,
                Assembly.GetExecutingAssembly(),
            };
        }

        public MetaChip(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public MetaChip(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        private Dictionary<string, Chips.ChipBase> Chips;

        private Dictionary<string, CircuitSim2.IO.InputBase> Inputs;
        private Dictionary<string, CircuitSim2.IO.OutputBase> Outputs;

        private Type FindType(TypeDescription TypeDesc, System.Type BaseType, System.Type[] ConstructorParams, System.Type[] RequiredInterfaces)
        {
            foreach (var assembly in AssemblySearchPath.Where(a => a.FullName == TypeDesc.AssemblyFullName))
            {
                foreach (var type in assembly.GetExportedTypes().Where(t => t.Namespace == TypeDesc.Namespace && t.Name == TypeDesc.Name && t.IsSubclassOf(BaseType)))
                {
                    bool iface_match = true;

                    if(RequiredInterfaces != null)
                    {
                        foreach(var iface in RequiredInterfaces)
                        {
                            if(!type.IsAssignableFrom(iface))
                            {
                                iface_match = false;
                                break;
                            }
                        }
                    }

                    if(!iface_match)
                    {
                        continue;
                    }

                    if (ConstructorParams == null)
                    {
                        return type;
                    }

                    var ctors = type.GetConstructors();

                    foreach (var ctor in ctors.Where(c => c.IsPublic))
                    {
                        var p = ctor.GetParameters();

                        if (p.Length != ConstructorParams.Length) continue;

                        bool ctor_match = true;
                        for (int i = 0; i < p.Length; i++)
                        {
                            if (p[i].ParameterType != ConstructorParams[i])
                            {
                                ctor_match = false;
                                break;
                            }
                        }

                        if (ctor_match)
                        {
                            return type;
                        }
                    }
                }
            }

            throw new ArgumentException($"Failed to locate type {TypeDesc.Namespace}.{TypeDesc.Name}");
        }

        public void Load(MetaChipDescription Description)
        {
            Detach();
            foreach (var subchip in SubChips)
            {
                RemoveSubChip(subchip);
                subchip.Dispose();
            }

            Chips = new Dictionary<string, ChipBase>();

            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            Inputs = new Dictionary<string, CircuitSim2.IO.InputBase>();


            var InputList = new List<CircuitSim2.IO.InputBase>();
            var input_idx = 0;
            foreach (var desc in Description.Inputs)
            {
                var input_type = typeof(CircuitSim2.IO.Input<>).MakeGenericType(FindType(desc.Type, typeof(object), null, null)); // TODO: ensure input_type implements IEquatable<input_type>?
                var input = Activator.CreateInstance(input_type, desc.Name, this, input_idx) as CircuitSim2.IO.InputBase;

                Inputs[desc.Name] = input;
                InputList.Add(input);
            }
            InputSet = new CircuitSim2.IO.InputSetBase(InputList);

            Outputs = new Dictionary<string, CircuitSim2.IO.OutputBase>();

            var OutputList = new List<CircuitSim2.IO.OutputBase>();
            var output_idx = 0;
            foreach (var desc in Description.Outputs)
            {
                var output_type = typeof(CircuitSim2.IO.Output<>).MakeGenericType(FindType(desc.Type, typeof(object), null, null)); // TODO: ensure output_type implements IEquatable<output_type>?
                var output = Activator.CreateInstance(output_type, desc.Name, this, output_idx) as CircuitSim2.IO.OutputBase;

                Outputs[desc.Name] = output;
                OutputList.Add(output);

                output_idx++;
            }
            OutputSet = new CircuitSim2.IO.OutputSetBase(OutputList);

            var chip_ctor_types = new System.Type[]
            {
                typeof(ChipBase),
                typeof(Engine.Engine),
            };

            foreach (var desc in Description.Chips)
            {
                var chip_type = FindType(desc.Type, typeof(CircuitSim2.Chips.ChipBase), chip_ctor_types, null);
                var chip = Activator.CreateInstance(chip_type, this as ChipBase, this.Engine) as ChipBase;

                chip.AutoTick = desc.AutoTick;
                chip.Position = desc.Position;
                chip.Rotation = desc.Rotation;
                chip.Scale = desc.Scale;

                AddSubChip(Chips[desc.ID] = chip);

                desc.Bindings?.ForEach(binding => Chips[desc.ID].InputSet[binding.Name].Bind(Inputs[binding.BindName]));
            }

            foreach (var desc in Description.Connections)
            {
                Chips[desc.DestID].InputSet[desc.DestInput].Attach(Chips[desc.SrcID].OutputSet[desc.SrcOutput]);
            }

            foreach (var desc in Description.Outputs)
            {
                OutputSet[desc.Name].Bind(Chips[desc.MapID].OutputSet[desc.MapOutput]);
            }
        }
    }
}
