using System.Linq;

using CircuitSim2.IO;
using CircuitSim2.Chips.Digital.Logic;
using System;

namespace CircuitSim2.Chips.Components.Adders
{
    [Chip("HalfAdder")]
    [Serializable]
    [PureChip]
    public sealed class HalfAdder : ChipBase
    {
        public sealed class OutputType : OutputSetBase
        {
            public readonly Output<bool> S;
            public readonly Output<bool> C;

            public OutputType(ChipBase Chip) : base(new OutputBase[] { new Output<bool>("S", Chip, 0), new Output<bool>("C", Chip, 1) })
            {
                S = this["S"] as Output<bool>;
                C = this["C"] as Output<bool>;
            }
        }

        [NonSerialized]
        public readonly GenericInput<bool, bool> Inputs;
        [NonSerialized]
        public readonly OutputType Outputs;

        [NonSerialized]
        private readonly XOR XOR;
        [NonSerialized]
        private readonly AND AND;

        public HalfAdder()
        {
            InputSet = (Inputs = new GenericInput<bool, bool>(this));
            OutputSet = (Outputs = new OutputType(this));

            AddSubChip(XOR = new XOR()
            {
                Position = new PositionVec
                {
                    X = 0,
                    Y = 1.0,
                    Z = 0,
                },
                Scale = 0.5,
            });
            AddSubChip(AND = new AND()
            {
                Position = new PositionVec
                {
                    X = 0,
                    Y = 1.0,
                    Z = 0,
                },
                Scale = 0.5,
            });

            XOR.Inputs.A.Bind(Inputs.A);
            XOR.Inputs.B.Bind(Inputs.B);

            AND.Inputs.A.Bind(Inputs.A);
            AND.Inputs.B.Bind(Inputs.B);

            Outputs.S.Bind(XOR.Outputs.Out);
            Outputs.C.Bind(AND.Outputs.Out);
        }
    }

    [Chip("FullAdder")]
    [Serializable]
    [PureChip]
    public sealed class FullAdder : ChipBase
    {
        public sealed class InputType : InputSetBase
        {
            public readonly Input<bool> A;
            public readonly Input<bool> B;
            public readonly Input<bool> Cin;

            public InputType(ChipBase Chip) : base(new InputBase[] { new Input<bool>("A", Chip, 0), new Input<bool>("B", Chip, 1), new CircuitSim2.IO.Input<bool>("Cin", Chip, 2) })
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

            public OutputType(ChipBase Chip) : base(new OutputBase[] { new Output<bool>("S", Chip, 0), new Output<bool>("Cout", Chip, 1) })
            {
                S = this["S"] as Output<bool>;
                Cout = this["Cout"] as Output<bool>;
            }
        }

        [NonSerialized]
        public readonly InputType Inputs;
        [NonSerialized]
        public readonly OutputType Outputs;

        [NonSerialized]
        private readonly XOR XOR1;
        [NonSerialized]
        private readonly XOR XOR2;
        [NonSerialized]
        private readonly AND AND1;
        [NonSerialized]
        private readonly AND AND2;
        [NonSerialized]
        private readonly OR OR;

        public FullAdder()
        {
            InputSet = (Inputs = new InputType(this));
            OutputSet = (Outputs = new OutputType(this));

            AddSubChip(XOR1 = new XOR()
            {
                Position = new PositionVec
                {
                    X = -4.5,
                    Y = 2.5,
                    Z = 0.0,
                },
                Scale = 0.5,
            });
            AddSubChip(XOR2 = new XOR()
            {
                Position = new PositionVec
                {
                    X = 2.0,
                    Y = 1.5,
                    Z = 0.0,
                },
                Scale = 0.5,
            });
            AddSubChip(AND1 = new AND()
            {
                Position = new PositionVec
                {
                    X = -1.5,
                    Y = 0.0,
                    Z = 0.0,
                },
                Scale = 0.5,
            });
            AddSubChip(AND2 = new AND()
            {
                Position = new PositionVec
                {
                    X = -4.5,
                    Y = -2.5,
                    Z = 0.0,
                },
                Scale = 0.5,
            });
            AddSubChip(OR = new OR()
            {
                Position = new PositionVec
                {
                    X = 4.5,
                    Y = -1.5,
                    Z = 0.0,
                },
                Scale = 0.5,
            });

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
    [Serializable]
    [PureChip]
    public sealed class ByteAdder : ChipBase
    {
        public sealed class InputType : InputSetBase
        {
            public readonly Input<byte> A;
            public readonly Input<byte> B;
            public readonly Input<bool> Cin;

            public InputType(ChipBase Chip) : base(new InputBase[] { new Input<byte>("A", Chip, 0), new Input<byte>("B", Chip, 1), new Input<bool>("Cin", Chip, 2) })
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

            public OutputType(ChipBase Chip) : base(new OutputBase[] { new Output<byte>("S", Chip, 0), new Output<bool>("Cout", Chip, 1) })
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

        public ByteAdder()
        {
            InputSet = (Inputs = new InputType(this));
            OutputSet = (Outputs = new OutputType(this));

            AddSubChip(DecomposerA = new Byte.Conversion.Decompose()
            {
                ParentChip = this,
                Position = new PositionVec
                {
                    X = -12.0,
                    Y = -10.0,
                    Z = 0.0,
                },
                Scale = 0.5,
            });
            AddSubChip(DecomposerB = new Byte.Conversion.Decompose()
            {
                ParentChip = this,
                Position = new PositionVec
                {
                    X = -12.0,
                    Y = 10.0,
                    Z = 0.0,
                },
                Scale = 0.5,
            });
            AddSubChip(Composer = new Byte.Conversion.Compose()
            {
                ParentChip = this,
                Position = new PositionVec
                {
                    X = 12.0,
                    Y = 0.0,
                    Z = 0.0,
                },
                Scale = 0.5,
            });

            Adders = new FullAdder[8];

            for (int i = 0; i < 8; i++)
            {/*
                if (i < num_ios / 2)
                {
                    y_pos = -y_space_per_io * (i + 0.5);
                }
                else
                {
                    y_pos = y_space_per_io * (i - 8 / 2 + 0.5);
                }*/

                AddSubChip(Adders[i] = new FullAdder()
                {
                    ParentChip = this,
                    Position = new PositionVec
                    {
                        X = 0.0,
                        Y = (size.Width / 8) * -(8 - i - 0.5) + size.Width / 2,
                        //Y = (i < 5 ? (size.Width/8)*-(4-i)+size.Width/2 : (size.Width/8)*(i-4)) + 3,
                        Z = 0.0,
                    },
                    Scale = 0.5,
                });
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

        public override SizeVec size => new SizeVec
        {
            Length = 32,
            Width = 48,
            Height = 1,
        };
    }
}
