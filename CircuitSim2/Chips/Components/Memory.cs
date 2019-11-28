using System;

using CircuitSim2.Chips.Digital.Logic;
using CircuitSim2.IO;

namespace CircuitSim2.Chips.Components.Memory
{
    [Chip("DLatch")]
    [Serializable]
    public sealed class DLatch : ChipBase
    {
        public sealed class InputType : InputSetBase
        {
            public readonly Input<bool> D;
            public readonly Input<bool> E;

            public InputType(ChipBase Chip) : base(new InputBase[] { new Input<bool>("D", Chip, 0), new Input<bool>("E", Chip, 1) })
            {
                D = this["D"] as Input<bool>;
                E = this["E"] as Input<bool>;
            }
        }

        [NonSerialized]
        public readonly InputType Inputs;

        public sealed class OutputType : OutputSetBase
        {
            public readonly Output<bool> Q;
            public readonly Output<bool> NQ;

            public OutputType(ChipBase Chip) : base(new OutputBase[] { new Output<bool>("Q", Chip, 0), new Output<bool>("NQ", Chip, 1) })
            {
                Q = this["Q"] as Output<bool>;
                NQ = this["NQ"] as Output<bool>;
            }
        }

        [NonSerialized]
        public readonly OutputType Outputs;

        [NonSerialized]
        private readonly NOT NOT;
        [NonSerialized]
        private readonly AND AND1;
        [NonSerialized]
        private readonly AND AND2;
        [NonSerialized]
        private readonly NOR NOR1;
        [NonSerialized]
        private readonly NOR NOR2;


        public DLatch()
        {
            InputSet = (Inputs = new InputType(this));
            OutputSet = (Outputs = new OutputType(this));

            AddSubChip(NOT = new NOT()
            {
                ParentChip = this,
                Position = new PositionVec
                {
                    X = -2.5,
                    Y = 2.5,
                    Z = 0.0,
                },
                Scale = 0.5,
            });
            AddSubChip(AND1 = new AND()
            {
                ParentChip = this,
                Position = new PositionVec
                {
                    X = -1.5,
                    Y = 1.5,
                    Z = 0.0,
                },
                Scale = 0.5,
            });
            AddSubChip(AND2 = new AND()
            {
                ParentChip = this,
                Position = new PositionVec
                {
                    X = -1.5,
                    Y = -1.5,
                    Z = 0.0,
                },
                Scale = 0.5,
            });
            AddSubChip(NOR1 = new NOR()
            {
                ParentChip = this,
                Position = new PositionVec
                {
                    X = 1.5,
                    Y = 1.5,
                    Z = 0.0,
                },
                Scale = 0.5,
            });
            AddSubChip(NOR2 = new NOR()
            {
                ParentChip = this,
                Position = new PositionVec
                {
                    X = 1.5,
                    Y = -1.5,
                    Z = 0.0,
                },
                Scale = 0.5,
            });

            NOT.Inputs.A.Bind(Inputs.D);
            AND2.Inputs.B.Bind(Inputs.D);

            AND1.Inputs.B.Bind(Inputs.E);
            AND2.Inputs.A.Bind(Inputs.E);

            AND1.Inputs.A.Attach(NOT.Outputs.Out);
            NOR1.Inputs.A.Attach(AND1.Outputs.Out);
            NOR2.Inputs.B.Attach(AND2.Outputs.Out);
            NOR1.Inputs.B.Attach(NOR2.Outputs.Out);
            NOR2.Inputs.A.Attach(NOR1.Outputs.Out);

            Outputs.Q.Bind(NOR1.Outputs.Out);
            Outputs.NQ.Bind(NOR2.Outputs.Out);
        }
    }
}
