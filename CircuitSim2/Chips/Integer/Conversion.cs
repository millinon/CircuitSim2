using CircuitSim2.Chips.Functors;
using CircuitSim2.IO;
using System;
using System.Linq;

namespace CircuitSim2.Chips.Integer.Conversion
{
    [PureChip("IntegerToByte")]
    public sealed class ToByte : UnaryFunctor<int, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base(a => (byte)a, Engine)
        {

        }
    }

    [PureChip("IntegerToChar")]
    public sealed class ToChar : UnaryFunctor<int, char>
    {
        public ToChar(Engine.Engine Engine = null) : base(a => (char)a, Engine)
        {

        }
    }

    [PureChip("IntegerToLong")]
    public sealed class ToLong : UnaryFunctor<int, long>
    {
        public ToLong(Engine.Engine Engine = null) : base(a => a, Engine)
        {

        }
    }

    [PureChip("IntegerToSingle")]
    public sealed class ToSingle : UnaryFunctor<int, float>
    {
        public ToSingle(Engine.Engine Engine = null) : base(a => a, Engine)
        {

        }
    }

    [PureChip("IntegerToDouble")]
    public sealed class ToDouble : UnaryFunctor<int, double>
    {
        public ToDouble(Engine.Engine Engine = null) : base(a => a, Engine)
        {

        }
    }

    [PureChip("IntegerToString")]
    public sealed class ToString : UnaryFunctor<int, string>
    {
        public ToString(Engine.Engine Engine = null) : base(a => a.ToString(), Engine)
        {

        }
    }

    [PureChip("IntegerToHexString")]
    public sealed class ToHexString : UnaryFunctor<int, string>
    {
        public ToHexString(Engine.Engine Engine = null) : base(a => a.ToString("X"), Engine)
        {

        }
    }

    [PureChip("IntegerDecompose")]
    public sealed class Decompose : ChipBase
    {
        public readonly GenericInput<int> Inputs;

        public sealed class OutputType : OutputSetBase
        {
            public readonly Output<byte> Byte0;
            public readonly Output<byte> Byte1;
            public readonly Output<byte> Byte2;
            public readonly Output<byte> Byte3;

            public OutputType(ChipBase Chip) : base(Enumerable.Range(0, 4).Select(i => new Output<byte>($"Byte{i}", Chip)))
            {
                Byte0 = this["Byte0"] as Output<byte>;
                Byte1 = this["Byte1"] as Output<byte>;
                Byte2 = this["Byte2"] as Output<byte>;
                Byte3 = this["Byte3"] as Output<byte>;
            }
        }

        public readonly OutputType Outputs;

        public Decompose(Engine.Engine Engine = null) : base(Engine)
        {
            InputSet = (Inputs = new GenericInput<int>(this));
            OutputSet = (Outputs = new OutputType(this));
        }

        private byte[] bytes;

        public override void Compute()
        {
            bytes = BitConverter.GetBytes(Inputs.A.Value);
        }

        public override void Output()
        {
            Outputs.Byte0.Value = bytes[0];
            Outputs.Byte1.Value = bytes[1];
            Outputs.Byte2.Value = bytes[2];
            Outputs.Byte3.Value = bytes[3];
        }
    }

    [PureChip("IntegerCompose")]
    public sealed class Compose : ChipBase
    {
        public sealed class InputType : InputSetBase
        {
            public readonly Input<byte> Byte0;
            public readonly Input<byte> Byte1;
            public readonly Input<byte> Byte2;
            public readonly Input<byte> Byte3;

            public InputType(ChipBase Chip) : base(Enumerable.Range(0, 4).Select(i => new Input<byte>($"Byte{i}", Chip)))
            {
                Byte0 = this["Byte0"] as Input<byte>;
                Byte1 = this["Byte1"] as Input<byte>;
                Byte2 = this["Byte2"] as Input<byte>;
                Byte3 = this["Byte3"] as Input<byte>;
            }
        }

        public readonly InputType Inputs;

        public readonly GenericOutput<int> Outputs;

        public Compose(Engine.Engine Engine = null) : base(Engine)
        {
            InputSet = (Inputs = new InputType(this));
            OutputSet = (Outputs = new GenericOutput<int>(this));
        }

        private int _out;

        public override void Compute()
        {
            var bytes = new byte[4] { Inputs.Byte0.Value, Inputs.Byte1.Value, Inputs.Byte2.Value, Inputs.Byte3.Value };
            _out = BitConverter.ToInt32(bytes,0);
        }

        public override void Output()
        {
            Outputs.Out.Value = _out;
        }
    }
}
