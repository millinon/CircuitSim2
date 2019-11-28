using CircuitSim2.Chips.Functors;
using System;

namespace CircuitSim2.Chips.Double.Conversion
{
    [Chip("DoubleToByte")]
    [Serializable]
    public sealed class ToByte : UnaryFunctor<double, byte>
    {
        public override byte Func(double Value) => (byte)Value;
    }

    [Chip("DoubleToChar")]
    [Serializable]
    public sealed class ToChar : UnaryFunctor<double, char>
    {
        public override char Func(double Value) => (char)Value;
    }

    [Chip("DoubleToLong")]
    [Serializable]
    public sealed class ToLong : UnaryFunctor<double, long>
    {
        public override long Func(double Value) => (long)Value;
    }

    [Chip("DoubleToSingle")]
    [Serializable]
    public sealed class ToSingle : UnaryFunctor<double, float>
    {
        public override float Func(double Value) => (float)Value;
    }

    [Chip("DoubleToInteger")]
    [Serializable]
    public sealed class ToInteger : UnaryFunctor<double, int>
    {
        public override int Func(double Value) => (int)Value;
    }

    [Chip("DoubleToString")]
    [Serializable]
    public sealed class ToString : UnaryFunctor<double, string>
    {
        public override string Func(double Value) => Value.ToString();
    }

    [Chip("DoubleToHexString")]
    [Serializable]
    public sealed class ToHexString : UnaryFunctor<double, string>
    {
        public override string Func(double Value) => Value.ToString("X");
    }
}
