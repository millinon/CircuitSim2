using CircuitSim2.Chips.Digital.Logic;
using CircuitSim2.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSim2.Chips.Components.Memory
{
    [Chip("DLatch")]
    public sealed class DLatch : ChipBase
    {
        public sealed class InputType : InputSetBase
        {
            public readonly Input<bool> D;
            public readonly Input<bool> E;

            public InputType(ChipBase Chip) : base(new InputBase[] { new Input<bool>("D", Chip), new Input<bool>("E", Chip) })
            {
                D = this["D"] as Input<bool>;
                E = this["E"] as Input<bool>;
            }
        }

        public readonly InputType Inputs;
        
        public sealed class OutputType : OutputSetBase
        {
            public readonly Output<bool> Q;
            public readonly Output<bool> NQ;

            public OutputType(ChipBase Chip) : base(new OutputBase[] { new Output<bool>("Q", Chip), new Output<bool>("NQ", Chip) })
            {
                Q = this["Q"] as Output<bool>;
                NQ = this["NQ"] as Output<bool>;
            }
        }

        public readonly OutputType Outputs;

        private readonly NOT NOT;
        private readonly AND AND1;
        private readonly AND AND2;
        private readonly NOR NOR1;
        private readonly NOR NOR2;

        public DLatch(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            if (Engine == null) throw new ArgumentNullException(nameof(Engine));

            InputSet = (Inputs = new InputType(this));
            OutputSet = (Outputs = new OutputType(this));

            NOT = new NOT(Engine);
            AND1 = new AND(Engine);
            AND2 = new AND(Engine);
            NOR1 = new NOR(Engine);
            NOR2 = new NOR(Engine);

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

        public DLatch(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }
    }
}
