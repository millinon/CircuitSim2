using CircuitSim2.Chips.Functors;
using CircuitSim2.IO;
using System;
using System.Linq;

namespace CircuitSim2.Chips.Integer.Conversion
{
    [Chip("IntegerToByte")]
    [Serializable]
    public sealed class ToByte : UnaryFunctor<int, byte>
    {
        public override byte Func(int Value) => (byte)Value;
    }

    [Chip("IntegerToChar")]
    [Serializable]
    public sealed class ToChar : UnaryFunctor<int, char>
    {
        public override char Func(int Value) => (char)Value;
    }

    [Chip("IntegerToLong")]
    [Serializable]
    public sealed class ToInteger : UnaryFunctor<int, long>
    {
        public override long Func(int Value) => Value;
    }

    [Chip("IntegerToSingle")]
    [Serializable]
    public sealed class ToSingle : UnaryFunctor<int, float>
    {
        public override float Func(int Value) => Value;
    }

    [Chip("IntegerToDouble")]
    [Serializable]
    public sealed class ToDouble : UnaryFunctor<int, double>
    {
        public override double Func(int Value) => Value;
    }

    [Chip("IntegerToString")]
    [Serializable]
    public sealed class ToString : UnaryFunctor<int, string>
    {
        public override string Func(int Value) => Value.ToString();
    }

    [Chip("IntegerToHexString")]
    [Serializable]
    public sealed class ToHexString : UnaryFunctor<int, string>
    {
        public override string Func(int Value) => Value.ToString("X");
    }

    [Chip("IntegerDecompose")]
    [Serializable]
    [PureChip]
    public sealed class Decompose : ChipBase
    {
        [NonSerialized]
        public readonly GenericInput<int> Inputs;

        public sealed class OutputType : OutputSetBase
        {
            public readonly Output<byte> Byte0;
            public readonly Output<byte> Byte1;
            public readonly Output<byte> Byte2;
            public readonly Output<byte> Byte3;

            public OutputType(ChipBase Chip) : base(Enumerable.Range(0, 4).Select(i => new Output<byte>($"Byte{i}", Chip, i)))
            {
                Byte0 = this["Byte0"] as Output<byte>;
                Byte1 = this["Byte1"] as Output<byte>;
                Byte2 = this["Byte2"] as Output<byte>;
                Byte3 = this["Byte3"] as Output<byte>;
            }
        }

        [NonSerialized]
        public readonly OutputType Outputs;


        public Decompose()
        {
            InputSet = (Inputs = new GenericInput<int>(this));
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
        }
    }

    [Chip("IntegerCompose")]
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

            public InputType(ChipBase Chip) : base(Enumerable.Range(0, 4).Select(i => new Input<byte>($"Byte{i}", Chip, i)))
            {
                Byte0 = this["Byte0"] as Input<byte>;
                Byte1 = this["Byte1"] as Input<byte>;
                Byte2 = this["Byte2"] as Input<byte>;
                Byte3 = this["Byte3"] as Input<byte>;
            }
        }
        
        [NonSerialized]
        public readonly InputType Inputs;

        [NonSerialized]
        public readonly GenericOutput<int> Outputs;

        public Compose()
        {
            InputSet = (Inputs = new InputType(this));
            OutputSet = (Outputs = new GenericOutput<int>(this));
        }

        [NonSerialized]
        private int _out;

        public override void Compute()
        {
            var bytes = new byte[4] { Inputs.Byte0.Value, Inputs.Byte1.Value, Inputs.Byte2.Value, Inputs.Byte3.Value };
            _out = BitConverter.ToInt32(bytes, 0);
        }

        public override void Commit()
        {
            Outputs.Out.Value = _out;
        }
    }
}
