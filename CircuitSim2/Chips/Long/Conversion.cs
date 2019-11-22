using CircuitSim2.Chips.Functors;
using CircuitSim2.IO;
using System;
using System.Linq;

namespace CircuitSim2.Chips.Long.Conversion
{
    [Chip("LongToByte")]
    public sealed class ToByte : UnaryFunctor<long, byte>
    {
        public ToByte(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToByte(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToByte(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override byte Func(long Value) => (byte)Value;
    }

    [Chip("LongToChar")]
    public sealed class ToChar : UnaryFunctor<long, char>
    {
        public ToChar(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToChar(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToChar(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override char Func(long Value) => (char)Value;
    }

    [Chip("LongToInteger")]
    public sealed class ToInteger : UnaryFunctor<long, int>
    {
        public ToInteger(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToInteger(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToInteger(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override int Func(long Value) => (int)Value;
    }

    [Chip("LongToSingle")]
    public sealed class ToSingle : UnaryFunctor<long, float>
    {
        public ToSingle(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToSingle(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToSingle(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override float Func(long Value) => Value;
    }

    [Chip("LongToDouble")]
    public sealed class ToDouble : UnaryFunctor<long, double>
    {
        public ToDouble(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToDouble(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToDouble(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override double Func(long Value) => Value;
    }

    [Chip("LongToString")]
    public sealed class ToString : UnaryFunctor<long, string>
    {
        public ToString(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToString(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToString(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override string Func(long Value) => Value.ToString();
    }

    [Chip("LongToHexString")]
    public sealed class ToHexString : UnaryFunctor<long, string>
    {
        public ToHexString(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToHexString(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToHexString(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override string Func(long Value) => Value.ToString("X");
    }

    [Chip("LongToDateTime")]
    public sealed class ToDateTime : UnaryFunctor<long, System.DateTime>
    {
        public ToDateTime(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToDateTime(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToDateTime(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override System.DateTime Func(long Value) => new System.DateTime(Value);
    }

    [Chip("LongDecompose")]
    [PureChip]
    public sealed class Decompose : ChipBase
    {
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

        public readonly OutputType Outputs; 

        public Decompose(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Decompose(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public Decompose(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = (Inputs = new GenericInput<long>(this));
            OutputSet = (Outputs = new OutputType(this));
        }

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

        public Compose(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Compose(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public Compose(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
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
