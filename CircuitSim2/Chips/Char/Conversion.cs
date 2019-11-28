using CircuitSim2.Chips.Functors;
using System;

namespace CircuitSim2.Chips.Char.Conversion
{
    [Chip("CharToByte")]
    [Serializable]
    public sealed class ToByte : UnaryFunctor<char, byte>
    {
        public override byte Func(char Value) => (byte)Value;
    }

    [Chip("CharToInteger")]
    [Serializable]
    public sealed class ToInteger : UnaryFunctor<char, int>
    {
        public override int Func(char Value) => (int)Value;
    }

    [Chip("CharToLong")]
    [Serializable]
    public sealed class ToLong : UnaryFunctor<char, long>
    {
        public override long Func(char Value) => Value;
    }

    [Chip("CharToString")]
    [Serializable]
    public sealed class ToString : UnaryFunctor<char, string>
    {
        public override string Func(char Value) => Value.ToString();
    }
}
