using System.Linq;

using CircuitSim2.IO;
using CircuitSim2.Chips.Digital.Logic;

namespace CircuitSim2.Chips.Components.Adders
{
    [Chip("HalfAdder")]
    [PureChip]
    public sealed class HalfAdder : ChipBase
    {
        public sealed class OutputType : OutputSetBase
        {
            public readonly Output<bool> S;
            public readonly Output<bool> C;

            public OutputType(ChipBase Chip) : base(new OutputBase[] { new Output<bool>("S", Chip), new Output<bool>("C", Chip) })
            {
                S = this["S"] as Output<bool>;
                C = this["C"] as Output<bool>;
            }
        }

        public readonly GenericInput<bool, bool> Inputs;
        public readonly OutputType Outputs;

        private readonly XOR XOR;
        private readonly AND AND;

        public HalfAdder() : this(null, null)
        {
        }

        public HalfAdder(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public HalfAdder(Engine.Engine Engine) : this(null, Engine)
        {
        }

        private HalfAdder(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = (Inputs = new GenericInput<bool, bool>(this));
            OutputSet = (Outputs = new OutputType(this));

            XOR = new XOR(Engine);
            AND = new AND(Engine);

            XOR.Inputs.A.Bind(Inputs.A);
            XOR.Inputs.B.Bind(Inputs.B);

            AND.Inputs.A.Bind(Inputs.A);
            AND.Inputs.B.Bind(Inputs.B);

            Outputs.S.Bind(XOR.Outputs.Out);
            Outputs.C.Bind(AND.Outputs.Out);
        }
    }

    [Chip("FullAdder")]
    [PureChip]
    public sealed class FullAdder : ChipBase
    {
        public sealed class InputType : InputSetBase
        {
            public readonly Input<bool> A;
            public readonly Input<bool> B;
            public readonly Input<bool> Cin;

            public InputType(ChipBase Chip) : base(new InputBase[] { new Input<bool>("A", Chip), new Input<bool>("B", Chip), new CircuitSim2.IO.Input<bool>("Cin", Chip) })
            {
                A = this["A"] as Input<bool>;
                B = this["B"] as Input<bool>;
                Cin = this["Cin"] as Input<bool>;
            }
        }


        public sealed class OutputType : OutputSetBase
        {
            public readonly Output<bool> S;
            public readonly Output<bool> Cout;

            public OutputType(ChipBase Chip) : base(new OutputBase[] { new Output<bool>("S", Chip), new Output<bool>("Cout", Chip) })
            {
                S = this["S"] as Output<bool>;
                Cout = this["Cout"] as Output<bool>;
            }
        }

        public readonly InputType Inputs;
        public readonly OutputType Outputs;

        private readonly XOR XOR1;
        private readonly XOR XOR2;
        private readonly AND AND1;
        private readonly AND AND2;
        private readonly OR OR;

        public FullAdder() : this(null, null)
        {
        }

        public FullAdder(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public FullAdder(Engine.Engine Engine) : this(null, Engine)
        {
        }

        private FullAdder(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            Inputs = new InputType(this);
            Outputs = new OutputType(this);

            InputSet = Inputs;
            OutputSet = Outputs;

            XOR1 = new XOR(Engine);
            XOR2 = new XOR(Engine);
            AND1 = new AND(Engine);
            AND2 = new AND(Engine);
            OR = new OR(Engine);

            XOR1.Inputs.A.Bind(Inputs.A);
            XOR1.Inputs.B.Bind(Inputs.B);
            XOR2.Inputs.B.Bind(Inputs.Cin);
            AND1.Inputs.B.Bind(Inputs.Cin);
            AND2.Inputs.A.Bind(Inputs.A);
            AND2.Inputs.B.Bind(Inputs.B);

            XOR2.Inputs.A.Attach(XOR1.Outputs.Out);
            AND1.Inputs.A.Attach(XOR1.Outputs.Out);
            OR.Inputs.A.Attach(AND1.Outputs.Out);
            OR.Inputs.B.Attach(AND2.Outputs.Out);

            Outputs.S.Bind(XOR2.Outputs.Out);
            Outputs.Cout.Bind(OR.Outputs.Out);
        }
    }

    [Chip("ByteAdder")]
    [PureChip]
    public sealed class ByteAdder : ChipBase
    {
        public sealed class InputType : InputSetBase
        {
            public readonly Input<byte> A;
            public readonly Input<byte> B;
            public readonly Input<bool> Cin;

            public InputType(ChipBase Chip) : base(new InputBase[] { new Input<byte>("A", Chip), new Input<byte>("B", Chip), new Input<bool>("Cin", Chip) })
            {
                A = this["A"] as Input<byte>;
                B = this["B"] as Input<byte>;
                Cin = this["Cin"] as Input<bool>;
            }
        }

        public sealed class OutputType : OutputSetBase
        {
            public readonly Output<byte> S;
            public readonly Output<bool> Cout;

            public OutputType(ChipBase Chip) : base(new OutputBase[] { new Output<byte>("S", Chip), new Output<bool>("Cout", Chip) })
            {
                S = this["S"] as Output<byte>;
                Cout = this["Cout"] as Output<bool>;
            }
        }

        public readonly InputType Inputs;
        public readonly OutputType Outputs;

        private readonly Byte.Conversion.Decompose DecomposerA;
        private readonly Byte.Conversion.Decompose DecomposerB;
        private readonly Byte.Conversion.Compose Composer;

        private readonly FullAdder[] Adders;

        public ByteAdder() : this(null, null)
        {
        }

        public ByteAdder(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ByteAdder(Engine.Engine Engine) : this(null, Engine)
        {
        }

        private ByteAdder(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = (Inputs = new InputType(this));
            OutputSet = (Outputs = new OutputType(this));

            DecomposerA = new Byte.Conversion.Decompose(Engine);
            DecomposerB = new Byte.Conversion.Decompose(Engine);
            Composer = new Byte.Conversion.Compose(Engine);

            Adders = new FullAdder[8];

            for (int i = 0; i < 8; i++)
            {
                Adders[i] = new FullAdder(Engine);
            }

            DecomposerA.Inputs.A.Bind(Inputs.A);
            DecomposerB.Inputs.A.Bind(Inputs.B);

            for (int i = 0; i < 8; i++)
            {
                Adders[i].Inputs.A.Attach(DecomposerA.Outputs[$"Bit{i}"]);
                Adders[i].Inputs.B.Attach(DecomposerB.Outputs[$"Bit{i}"]);
            }

            Adders[0].Inputs.Cin.Bind(Inputs.Cin);
            for (int i = 1; i < 8; i++)
            {
                Adders[i].Inputs.Cin.Attach(Adders[i - 1].Outputs.Cout);
            }

            for (int i = 0; i < 8; i++)
            {
                Composer.Inputs[$"Bit{i}"].Attach(Adders[i].Outputs.S);
            }

            Outputs.S.Bind(Composer.Outputs.Out);
            Outputs.Cout.Bind(Adders[7].Outputs.Cout);
        }
    }
}
