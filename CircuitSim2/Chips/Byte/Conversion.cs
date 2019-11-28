using System;
using System.Linq;

using CircuitSim2.Chips.Functors;
using CircuitSim2.IO;

namespace CircuitSim2.Chips.Byte.Conversion
{
    [Chip("ByteToDouble")]
    [Serializable]
    public sealed class ToDouble : UnaryFunctor<byte, double>
    {
        public override double Func(byte Value) => (double)Value;
    }

    [Chip("ByteToChar")]
    [Serializable]
    public sealed class ToChar : UnaryFunctor<byte, char>
    {
        public override char Func(byte Value) => (char)Value;
    }

    [Chip("ByteToLong")]
    [Serializable]
    public sealed class ToLong : UnaryFunctor<byte, long>
    {
        public override long Func(byte Value) => (long)Value;
    }

    [Chip("ByteToSingle")]
    [Serializable]
    public sealed class ToSingle : UnaryFunctor<byte, float>
    {
        public override float Func(byte Value) => (float)Value;
    }

    [Chip("ByteToInteger")]
    [Serializable]
    public sealed class ToInteger : UnaryFunctor<byte, int>
    {
        public override int Func(byte Value) => (int)Value;
    }

    [Chip("ByteToString")]
    [Serializable]
    public sealed class ToString : UnaryFunctor<byte, string>
    {
        public override string Func(byte Value) => Value.ToString();
    }

    [Chip("ByteToHexString")]
    [Serializable]
    public sealed class ToHexString : UnaryFunctor<byte, string>
    {
        public override string Func(byte Value) => Value.ToString("X");
    }

    [Chip("ByteDecompose")]
    [Serializable]
    [PureChip]
    public sealed class Decompose : ChipBase
    {
        public sealed class OutputType : OutputSetBase
        {
            public readonly Output<bool> Bit0;
            public readonly Output<bool> Bit1;
            public readonly Output<bool> Bit2;
            public readonly Output<bool> Bit3;
            public readonly Output<bool> Bit4;
            public readonly Output<bool> Bit5;
            public readonly Output<bool> Bit6;
            public readonly Output<bool> Bit7;

            public OutputType(ChipBase Chip) : base(Enumerable.Range(0, 8).Select(i => new Output<bool>($"Bit{i}", Chip, i)))
            {
                Bit0 = this["Bit0"] as Output<bool>;
                Bit1 = this["Bit1"] as Output<bool>;
                Bit2 = this["Bit2"] as Output<bool>;
                Bit3 = this["Bit3"] as Output<bool>;
                Bit4 = this["Bit4"] as Output<bool>;
                Bit5 = this["Bit5"] as Output<bool>;
                Bit6 = this["Bit6"] as Output<bool>;
                Bit7 = this["Bit7"] as Output<bool>;
            }
        }

        [NonSerialized]
        public readonly GenericInput<byte> Inputs;
        [NonSerialized]
        public readonly OutputType Outputs;

        public Decompose()
        {
            Inputs = new GenericInput<byte>(this);
            Outputs = new OutputType(this);

            InputSet = Inputs;
            OutputSet = Outputs;
        }

        [NonSerialized]
        bool[] _out = new bool[8];
        public override void Compute()
        {
            byte b = Inputs.A.Value;

            _out[0] = (b & 0x1) == 0x1;
            _out[1] = (b & (0x1 << 1)) == (0x1 << 1);
            _out[2] = (b & (0x1 << 2)) == (0x1 << 2);
            _out[3] = (b & (0x1 << 3)) == (0x1 << 3);
            _out[4] = (b & (0x1 << 4)) == (0x1 << 4);
            _out[5] = (b & (0x1 << 5)) == (0x1 << 5);
            _out[6] = (b & (0x1 << 6)) == (0x1 << 6);
            _out[7] = (b & (0x1 << 7)) == (0x1 << 7);
        }

        public override void Commit()
        {
            Outputs.Bit0.Value = _out[0];
            Outputs.Bit1.Value = _out[1];
            Outputs.Bit2.Value = _out[2];
            Outputs.Bit3.Value = _out[3];
            Outputs.Bit4.Value = _out[4];
            Outputs.Bit5.Value = _out[5];
            Outputs.Bit6.Value = _out[6];
            Outputs.Bit7.Value = _out[7];
        }
    }

    [Chip("ByteCompose")]
    [Serializable]
    [PureChip]
    public sealed class Compose : ChipBase
    {
        public sealed class InputType : InputSetBase
        {
            public readonly Input<bool> Bit0;
            public readonly Input<bool> Bit1;
            public readonly Input<bool> Bit2;
            public readonly Input<bool> Bit3;
            public readonly Input<bool> Bit4;
            public readonly Input<bool> Bit5;
            public readonly Input<bool> Bit6;
            public readonly Input<bool> Bit7;

            public InputType(ChipBase Chip) : base(Enumerable.Range(0, 8).Select(i => new Input<bool>($"Bit{i}", Chip, i)))
            {
                Bit0 = this["Bit0"] as Input<bool>;
                Bit1 = this["Bit1"] as Input<bool>;
                Bit2 = this["Bit2"] as Input<bool>;
                Bit3 = this["Bit3"] as Input<bool>;
                Bit4 = this["Bit4"] as Input<bool>;
                Bit5 = this["Bit5"] as Input<bool>;
                Bit6 = this["Bit6"] as Input<bool>;
                Bit7 = this["Bit7"] as Input<bool>;
            }
        }

        [NonSerialized]
        public readonly InputType Inputs;
        [NonSerialized]
        public readonly GenericOutput<byte> Outputs;

        public Compose()
        {
            Inputs = new InputType(this);
            Outputs = new GenericOutput<byte>(this);

            InputSet = Inputs;
            OutputSet = Outputs;
        }

        [NonSerialized]
        private byte _out;
        public override void Compute()
        {
            _out = (byte)(
                (Inputs.Bit0.Value ? 0x1 : 0) |
                (Inputs.Bit1.Value ? (0x1 << 1) : 0) |
                (Inputs.Bit2.Value ? (0x1 << 2) : 0) |
                (Inputs.Bit3.Value ? (0x1 << 3) : 0) |
                (Inputs.Bit4.Value ? (0x1 << 4) : 0) |
                (Inputs.Bit5.Value ? (0x1 << 5) : 0) |
                (Inputs.Bit6.Value ? (0x1 << 6) : 0) |
                (Inputs.Bit7.Value ? (0x1 << 7) : 0)
                );
        }

        public override void Commit()
        {
            Outputs.Out.Value = _out;
        }
    }
}
