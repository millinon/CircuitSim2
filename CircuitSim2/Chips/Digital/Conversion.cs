using CircuitSim2.Chips.Functors;
using CircuitSim2.IO;
using System;

namespace CircuitSim2.Chips.Digital.Conversion
{
    public abstract class DigitalConverter<T> : ChipBase
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

        [NonSerialized]
        public readonly GenericInput<bool> Inputs;
        [NonSerialized]
        public readonly GenericOutput<T> Outputs;

        public DigitalConverter()
        {
            InputSet = (Inputs = new GenericInput<bool>(this));
            OutputSet = (Outputs = new GenericOutput<T>(this));
        }

        [NonSerialized]
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
    [Serializable]
    public sealed class ToByte : DigitalConverter<byte>
    {
    }

    [Chip("DigitalToChar")]
    [Serializable]
    public sealed class ToChar : DigitalConverter<char>
    {
    }

    [Chip("DigitalToDouble")]
    [Serializable]
    public sealed class ToDouble : DigitalConverter<double>
    {
    }

    [Chip("DigitalToInteger")]
    [Serializable]
    public sealed class ToInteger : DigitalConverter<int>
    {
    }

    [Chip("DigitalToLong")]
    [Serializable]
    public sealed class ToLong : DigitalConverter<long>
    {
    }

    [Chip("DigitalToSingle")]
    [Serializable]
    public sealed class ToSingle : DigitalConverter<float>
    {
    }

    [Chip("DigitalToString")]
    [Serializable]
    public sealed class ToString : DigitalConverter<string>
    {
        public ToString()
        {
            Low = "False";
            High = "True";
        }
    }
}
