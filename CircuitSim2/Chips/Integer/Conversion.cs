using CircuitSim2.Chips.Functors;
using CircuitSim2.IO;
using System;
using System.Linq;

namespace CircuitSim2.Chips.Integer.Conversion
{
    [Chip("IntegerToByte")]
    public sealed class ToByte : UnaryFunctor<int, byte>
    {
        private ToByte(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToByte(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToByte(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToByte() : this(null, null)
        {
        }

        public override byte Func(int Value) => (byte)Value;
    }

    [Chip("IntegerToChar")]
    public sealed class ToChar : UnaryFunctor<int, char>
    {
        private ToChar(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToChar(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToChar(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToChar() : this(null, null)
        {
        }

        public override char Func(int Value) => (char)Value;
    }

    [Chip("IntegerToLong")]
    public sealed class ToInteger : UnaryFunctor<int, long>
    {
        private ToInteger(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToInteger(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToInteger(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToInteger() : this(null, null)
        {
        }

        public override long Func(int Value) => Value;
    }

    [Chip("IntegerToSingle")]
    public sealed class ToSingle : UnaryFunctor<int, float>
    {
        private ToSingle(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToSingle(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToSingle(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToSingle() : this(null, null)
        {
        }

        public override float Func(int Value) => Value;
    }

    [Chip("IntegerToDouble")]
    public sealed class ToDouble : UnaryFunctor<int, double>
    {
        private ToDouble(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToDouble(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToDouble(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToDouble() : this(null, null)
        {
        }

        public override double Func(int Value) => Value;
    }

    [Chip("IntegerToString")]
    public sealed class ToString : UnaryFunctor<int, string>
    {
        private ToString(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToString(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToString(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToString() : this(null, null)
        {
        }

        public override string Func(int Value) => Value.ToString();
    }

    [Chip("IntegerToHexString")]
    public sealed class ToHexString : UnaryFunctor<int, string>
    {
        private ToHexString(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToHexString(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToHexString(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public ToHexString() : this(null, null)
        {
        }

        public override string Func(int Value) => Value.ToString("X");
    }

    [Chip("IntegerDecompose")]
    [PureChip]
    public sealed class Decompose : ChipBase
    {
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

        public readonly OutputType Outputs;
        public Decompose() : this(null, null)
        {
        }

        public Decompose(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Decompose(Engine.Engine Engine) : this(null, Engine)
        {
        }

        private Decompose(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
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

    [Chip("IntegerCompose")]
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

        public readonly InputType Inputs;

        public readonly GenericOutput<int> Outputs;

        public Compose() : this(null, null)
        {
        }

        public Compose(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Compose(Engine.Engine Engine) : this(null, Engine)
        {
        }

        private Compose(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
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
