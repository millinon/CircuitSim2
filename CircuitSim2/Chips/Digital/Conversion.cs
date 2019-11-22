using CircuitSim2.Chips.Functors;
using CircuitSim2.IO;
using System;

namespace CircuitSim2.Chips.Digital.Conversion
{
    public abstract class DigitalConverter<T> : ChipBase where T : IEquatable<T>
    {
        private T low;
        [ChipProperty]
        public T Low
        {
            get => low;
            set
            {
                low = value;

                Tick();
            }
        }

        private T high;
        [ChipProperty]
        public T High
        {
            get => high;
            set
            {
                high = value;

                Tick();
            }
        }

        public readonly GenericInput<bool> Inputs;
        public readonly GenericOutput<T> Outputs;

        public DigitalConverter(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = (Inputs = new GenericInput<bool>(this));
            OutputSet = (Outputs = new GenericOutput<T>(this));
        }

        private T _out;

        public override void Compute()
        {
            _out = Inputs.A.Value ? High : Low;
        }

        public override void Commit()
        {
            Outputs.Out.Value = _out;
        }

        public sealed override void Tick()
        {
            base.Tick();
        }
    }


    [Chip("DigitalToByte")]
    public sealed class ToByte : DigitalConverter<byte>
    {
        public ToByte(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToByte(ChipBase ParentChip) : base(ParentChip, ParentChip?.Engine)
        {
        }

        public ToByte(Engine.Engine Engine) : base(null, Engine)
        {
        }
    }

    [Chip("DigitalToChar")]
    public sealed class ToChar : DigitalConverter<char>
    {
        public ToChar(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToChar(ChipBase ParentChip) : base(ParentChip, ParentChip?.Engine)
        {
        }

        public ToChar(Engine.Engine Engine) : base(null, Engine)
        {
        }
    }

    [Chip("DigitalToDouble")]
    public sealed class ToDouble : DigitalConverter<double>
    {
        public ToDouble(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToDouble(ChipBase ParentChip) : base(ParentChip, ParentChip?.Engine)
        {
        }

        public ToDouble(Engine.Engine Engine) : base(null, Engine)
        {
        }
    }

    [Chip("DigitalToInteger")]
    public sealed class ToInteger : DigitalConverter<int>
    {
        public ToInteger(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToInteger(ChipBase ParentChip) : base(ParentChip, ParentChip?.Engine)
        {
        }

        public ToInteger(Engine.Engine Engine) : base(null, Engine)
        {
        }
    }

    [Chip("DigitalToLong")]
    public sealed class ToLong : DigitalConverter<long>
    {
        public ToLong(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToLong(ChipBase ParentChip) : base(ParentChip, ParentChip?.Engine)
        {
        }

        public ToLong(Engine.Engine Engine) : base(null, Engine)
        {
        }
    }

    [Chip("DigitalToSingle")]
    public sealed class ToSingle : DigitalConverter<float>
    {
        public ToSingle(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToSingle(ChipBase ParentChip) : base(ParentChip, ParentChip?.Engine)
        {
        }

        public ToSingle(Engine.Engine Engine) : base(null, Engine)
        {
        }
    }

    [Chip("DigitalToString")]
    public sealed class ToString : DigitalConverter<string>
    {
        public ToString(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            Low = "False";
            High = "True";
        }

        public ToString(ChipBase ParentChip) : base(ParentChip, ParentChip?.Engine)
        {
        }

        public ToString(Engine.Engine Engine) : base(null, Engine)
        {
        }
    }
}
