using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Digital.Conversion
{
    [Chip("DigitalToByte")]
    public sealed class ToByte : UnaryFunctor<bool, byte>
    {
        private readonly byte Low;
        private readonly byte High;

        private ToByte(byte Low, byte High, ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            this.Low = Low;
            this.High = High;
        }

        public ToByte(byte Low, byte High, ChipBase ParentChip) : this(Low, High, ParentChip, ParentChip?.Engine)
        {
        }

        public ToByte(byte Low, byte High, Engine.Engine Engine) : this(Low, High, null, Engine)
        {
        }

        public ToByte(byte Low = 0, byte High = 1) : this(Low, High, null, null)
        {
        }

        public override byte Func(bool Value) => Value ? High : Low;
    }

    [Chip("DigitalToChar")]
    public sealed class ToChar : UnaryFunctor<bool, char>
    {
        private readonly char Low;
        private readonly char High;

        private ToChar(char Low, char High, ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            this.Low = Low;
            this.High = High;
        }

        public ToChar(char Low, char High, ChipBase ParentChip) : this(Low, High, ParentChip, ParentChip?.Engine)
        {
        }

        public ToChar(char Low, char High, Engine.Engine Engine) : this(Low, High, null, Engine)
        {
        }

        public ToChar(char Low, char High) : this(Low, High, null, null)
        {
        }

        public override char Func(bool Value) => Value ? High : Low;
    }

    [Chip("DigitalToInteger")]
    public sealed class ToInteger : UnaryFunctor<bool, int>
    {
        private readonly int Low;
        private readonly int High;

        private ToInteger(int Low, int High, ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            this.Low = Low;
            this.High = High;
        }

        public ToInteger(int Low, int High, ChipBase ParentChip) : this(Low, High, ParentChip, ParentChip?.Engine)
        {
        }

        public ToInteger(int Low, int High, Engine.Engine Engine) : this(Low, High, null, Engine)
        {
        }

        public ToInteger(int Low = 0, int High = 1) : this(Low, High, null, null)
        {
        }

        public override int Func(bool Value) => Value ? High : Low;
    }

    [Chip("DigitalToLong")]
    public sealed class ToLong : UnaryFunctor<bool, long>
    {
        private readonly long Low;
        private readonly long High;

        private ToLong(long Low, long High, ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            this.Low = Low;
            this.High = High;
        }

        public ToLong(long Low, long High, ChipBase ParentChip) : this(Low, High, ParentChip, ParentChip?.Engine)
        {
        }

        public ToLong(long Low, long High, Engine.Engine Engine) : this(Low, High, null, Engine)
        {
        }

        public ToLong(long Low = 0, long High = 1) : this(Low, High, null, null)
        {
        }

        public override long Func(bool Value) => Value ? High : Low;
    }

    [Chip("DigitalToSingle")]
    public sealed class ToSingle : UnaryFunctor<bool, float>
    {
        private readonly float Low;
        private readonly float High;

        private ToSingle(float Low, float High, ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            this.Low = Low;
            this.High = High;
        }

        public ToSingle(float Low, float High, ChipBase ParentChip) : this(Low, High, ParentChip, ParentChip?.Engine)
        {
        }

        public ToSingle(float Low, float High, Engine.Engine Engine) : this(Low, High, null, Engine)
        {
        }

        public ToSingle(float Low = 0.0f, float High = 1.0f) : this(Low, High, null, null)
        {
        }

        public override float Func(bool Value) => Value ? High : Low;
    }

    [Chip("DigitalToDouble")]
    public sealed class ToDouble : UnaryFunctor<bool, double>
    {
        private readonly double Low;
        private readonly double High;

        private ToDouble(double Low, double High, ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            this.Low = Low;
            this.High = High;
        }

        public ToDouble(double Low, double High, ChipBase ParentChip) : this(Low, High, ParentChip, ParentChip?.Engine)
        {
        }

        public ToDouble(double Low, double High, Engine.Engine Engine) : this(Low, High, null, Engine)
        {
        }

        public ToDouble(double Low = 0.0, double High = 1.0) : this(Low, High, null, null)
        {
        }

        public override double Func(bool Value) => Value ? High : Low;
    }

    [Chip("DigitalToString")]
    public sealed class ToString : UnaryFunctor<bool, string>
    {
        private readonly string Low;
        private readonly string High;

        private ToString(string Low, string High, ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            this.Low = Low;
            this.High = High;
        }

        public ToString(string Low, string High, ChipBase ParentChip) : this(Low, High, ParentChip, ParentChip?.Engine)
        {
        }

        public ToString(string Low, string High, Engine.Engine Engine) : this(Low, High, null, Engine)
        {
        }

        public ToString(string Low = "False", string High = "True") : this(Low, High, null, null)
        {
        }

        public override string Func(bool Value) => Value ? High : Low;
    }
}
