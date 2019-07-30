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
        public sealed class MetaChipDescription
        {
            [Serializable]
            public sealed class InputDescription
            {
                public string Name;
                public CircuitSim2.IO.Type Type;
            }
            public List<InputDescription> Inputs;

            [Serializable]
            public sealed class OutputDescription
            {
                public string Name;
                public CircuitSim2.IO.Type Type;
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

                public string Namespace;
                public string Name;
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

        private MetaChip(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = new CircuitSim2.IO.NoInputs();
            OutputSet = new CircuitSim2.IO.NoOutputs();
        }

        public MetaChip(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public MetaChip(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public MetaChip() : this(null, null)
        {
        }
        
        private Dictionary<string, Chips.ChipBase> Chips;

        private Dictionary<string, CircuitSim2.IO.InputBase> Inputs;
        private Dictionary<string, CircuitSim2.IO.OutputBase> Outputs;

        public void Load(MetaChipDescription Description)
        {
            Detach();
            foreach(var subchip in SubChips)
            {
                RemoveSubChip(subchip);
                subchip.Dispose();
            }

            Chips = new Dictionary<string, ChipBase>();

            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            Inputs = new Dictionary<string, CircuitSim2.IO.InputBase>();

            var InputList = new List<CircuitSim2.IO.InputBase>();
            foreach (var desc in Description.Inputs)
            {
                CircuitSim2.IO.InputBase input;

                switch (desc.Type)
                {
                    case CircuitSim2.IO.Type.DIGITAL:
                        input = new CircuitSim2.IO.Input<bool>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.BYTE:
                        input = new CircuitSim2.IO.Input<byte>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.CHAR:
                        input = new CircuitSim2.IO.Input<char>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.INT:
                        input = new CircuitSim2.IO.Input<int>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.LONG:
                        input = new CircuitSim2.IO.Input<long>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.SINGLE:
                        input = new CircuitSim2.IO.Input<float>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.DOUBLE:
                        input = new CircuitSim2.IO.Input<double>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.STRING:
                        input = new CircuitSim2.IO.Input<string>(desc.Name, this);
                        break;

                    default:
                        throw new ArgumentException();

                }

                Inputs[desc.Name] = input;
                InputList.Add(input);
            }
            InputSet = new CircuitSim2.IO.InputSetBase(InputList);

            Outputs = new Dictionary<string, CircuitSim2.IO.OutputBase>();

            var OutputList = new List<CircuitSim2.IO.OutputBase>();
            foreach (var desc in Description.Outputs)
            {
                CircuitSim2.IO.OutputBase output;

                switch (desc.Type)
                {
                    case CircuitSim2.IO.Type.DIGITAL:
                        output = new CircuitSim2.IO.Output<bool>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.BYTE:
                        output = new CircuitSim2.IO.Output<byte>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.CHAR:
                        output = new CircuitSim2.IO.Output<char>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.INT:
                        output = new CircuitSim2.IO.Output<int>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.LONG:
                        output = new CircuitSim2.IO.Output<long>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.SINGLE:
                        output = new CircuitSim2.IO.Output<float>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.DOUBLE:
                        output = new CircuitSim2.IO.Output<double>(desc.Name, this);
                        break;

                    case CircuitSim2.IO.Type.STRING:
                        output = new CircuitSim2.IO.Output<string>(desc.Name, this);
                        break;

                    default:
                        throw new ArgumentException();
                }

                Outputs[desc.Name] = output;
                OutputList.Add(output);
            }
            OutputSet = new CircuitSim2.IO.OutputSetBase(OutputList);

            foreach (var desc in Description.Chips)
            {
                var matches = types.Where(type => type.Name == desc.Name && type.Namespace == desc.Namespace);

                if (!matches.Any()) throw new ArgumentException();

                var match = matches.First();

                AddSubChip(Chips[desc.ID] = Activator.CreateInstance(match, new object[] { (this as ChipBase) }) as Chips.ChipBase);
                Chips[desc.ID].AutoTick = desc.AutoTick;
                Chips[desc.ID].Position = desc.Position;
                Chips[desc.ID].Rotation = desc.Rotation;
                Chips[desc.ID].Scale = desc.Scale;



                desc.Bindings?.ForEach(binding => Chips[desc.ID].InputSet[binding.Name].Bind(Inputs[binding.BindName]));
            }

            foreach (var desc in Description.Connections)
            {
                Chips[desc.DestID].InputSet[desc.DestInput].Attach(Chips[desc.SrcID].OutputSet[desc.SrcOutput]);
            }

            foreach(var desc in Description.Outputs)
            {
                OutputSet[desc.Name].Bind(Chips[desc.MapID].OutputSet[desc.MapOutput]);
            }
        }
    }
}
