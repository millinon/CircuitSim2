using CircuitSim2.Chips.Functors;
using CircuitSim2.IO;
using System;
using System.Linq;

namespace CircuitSim2.Chips.Long.Conversion
{
    [Chip("LongToByte")]
    [Serializable]
    public sealed class ToByte : UnaryFunctor<long, byte>
    {
        public override byte Func(long Value) => (byte)Value;
    }

    [Chip("LongToChar")]
    [Serializable]
    public sealed class ToChar : UnaryFunctor<long, char>
    {
        public override char Func(long Value) => (char)Value;
    }

    [Chip("LongToInteger")]
    [Serializable]
    public sealed class ToInteger : UnaryFunctor<long, int>
    {
        public override int Func(long Value) => (int)Value;
    }

    [Chip("LongToSingle")]
    [Serializable]
    public sealed class ToSingle : UnaryFunctor<long, float>
    {
        public override float Func(long Value) => Value;
    }

    [Chip("LongToDouble")]
    [Serializable]
    public sealed class ToDouble : UnaryFunctor<long, double>
    {
        public override double Func(long Value) => Value;
    }

    [Chip("LongToString")]
    [Serializable]
    public sealed class ToString : UnaryFunctor<long, string>
    {
        public override string Func(long Value) => Value.ToString();
    }

    [Chip("LongToHexString")]
    [Serializable]
    public sealed class ToHexString : UnaryFunctor<long, string>
    {
        public override string Func(long Value) => Value.ToString("X");
    }

    [Chip("LongToDateTime")]
    [Serializable]
    public sealed class ToDateTime : UnaryFunctor<long, System.DateTime>
    {
        public override System.DateTime Func(long Value) => new System.DateTime(Value);
    }

    [Chip("LongDecompose")]
    [Serializable]
    [PureChip]
    public sealed class Decompose : ChipBase
    {
        [NonSerialized]
        public readonly GenericInput<long> Inputs;

        public sealed class OutputType : OutputSetBase
        {
            public readonly Output<byte> Byte0;
            public readonly Output<byte> Byte1;
            public readonly Output<byte> Byte2;
            public readonly Output<byte> Byte3;
            public readonly Output<byte> Byte4;
            public readonly Output<byte> Byte5;
            public readonly Output<byte> Byte6;
            public readonly Output<byte> Byte7;

            public OutputType(ChipBase Chip) : base(Enumerable.Range(0, 8).Select(i => new Output<byte>($"Byte{i}", Chip, i)))
            {
                Byte0 = this["Byte0"] as Output<byte>;
                Byte1 = this["Byte1"] as Output<byte>;
                Byte2 = this["Byte2"] as Output<byte>;
                Byte3 = this["Byte3"] as Output<byte>;
                Byte4 = this["Byte4"] as Output<byte>;
                Byte5 = this["Byte5"] as Output<byte>;
                Byte6 = this["Byte6"] as Output<byte>;
                Byte7 = this["Byte7"] as Output<byte>;
            }
        }

        [NonSerialized]
        public readonly OutputType Outputs;

        public Decompose()
        {
            InputSet = (Inputs = new GenericInput<long>(this));
            OutputSet = (Outputs = new OutputType(this));
        }

        [NonSerialized]
        private byte[] bytes;

        public override void Compute()
        {
            bytes = BitConverter.GetBytes(Inputs.A.Value);
        }

        public override void Commit()
        {
            Outputs.Byte0.Value = bytes[0];
            Outputs.Byte1.Value = bytes[1];
            Outputs.Byte2.Value = bytes[2];
            Outputs.Byte3.Value = bytes[3];
            Outputs.Byte4.Value = bytes[4];
            Outputs.Byte5.Value = bytes[5];
            Outputs.Byte6.Value = bytes[6];
            Outputs.Byte7.Value = bytes[7];
        }
    }

    [Chip("LongCompose")]
    [Serializable]
    [PureChip]
    public sealed class Compose : ChipBase
    {
        public sealed class InputType : InputSetBase
        {
            public readonly Input<byte> Byte0;
            public readonly Input<byte> Byte1;
            public readonly Input<byte> Byte2;
            public readonly Input<byte> Byte3;
            public readonly Input<byte> Byte4;
            public readonly Input<byte> Byte5;
            public readonly Input<byte> Byte6;
            public readonly Input<byte> Byte7;

            public InputType(ChipBase Chip) : base(Enumerable.Range(0, 8).Select(i => new Input<byte>($"Byte{i}", Chip, i)))
            {
                Byte0 = this["Byte0"] as Input<byte>;
                Byte1 = this["Byte1"] as Input<byte>;
                Byte2 = this["Byte2"] as Input<byte>;
                Byte3 = this["Byte3"] as Input<byte>;
                Byte4 = this["Byte4"] as Input<byte>;
                Byte5 = this["Byte5"] as Input<byte>;
                Byte6 = this["Byte6"] as Input<byte>;
                Byte7 = this["Byte7"] as Input<byte>;
            }
        }

        public readonly InputType Inputs;

        public readonly GenericOutput<long> Outputs;

        public Compose()
        {
            InputSet = (Inputs = new InputType(this));
            OutputSet = (Outputs = new GenericOutput<long>(this));
        }

        private long _out;

        public override void Compute()
        {
            var bytes = new byte[8] { Inputs.Byte0.Value, Inputs.Byte1.Value, Inputs.Byte2.Value, Inputs.Byte3.Value,
                                      Inputs.Byte4.Value, Inputs.Byte5.Value, Inputs.Byte6.Value, Inputs.Byte7.Value};
            _out = BitConverter.ToInt64(bytes, 0);
        }

        public override void Commit()
        {
            Outputs.Out.Value = _out;
        }
    }
}
