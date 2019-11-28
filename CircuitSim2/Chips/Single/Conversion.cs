using CircuitSim2.Chips.Functors;
using System;

namespace CircuitSim2.Chips.Single.Conversion
{
    [Chip("SingleToByte")]
    [Serializable]
    public sealed class ToByte : UnaryFunctor<float, byte>
    {
        public override byte Func(float Value) => (byte)Value;
    }

    [Chip("SingleToChar")]
    [Serializable]
    public sealed class ToChar : UnaryFunctor<float, char>
    {
        public override char Func(float Value) => (char)Value;
    }

    [Chip("SingleToInteger")]
    [Serializable]
    public sealed class ToInteger : UnaryFunctor<float, int>
    {
        public override int Func(float Value) => (int)Value;
    }

    [Chip("SingleToLong")]
    [Serializable]
    public sealed class ToLong : UnaryFunctor<float, long>
    {
        public override long Func(float Value) => (long)Value;
    }

    [Chip("SingleToDouble")]
    [Serializable]
    public sealed class ToDouble : UnaryFunctor<float, double>
    {
        public override double Func(float Value) => Value;
    }

    [Chip("SingleToString")]
    [Serializable]
    public sealed class ToString : UnaryFunctor<float, string>
    {
        public override string Func(float Value) => Value.ToString();
    }
}
