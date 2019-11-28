using CircuitSim2.Chips.Functors;
using System;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<char, char, char>;

namespace CircuitSim2.Chips.Char.Arithmetic
{
    [Chip("CharAdd")]
    [Serializable]
    public sealed class Add : BF
    {
        public override char Func(char Val1, char Val2) => (char)(Val1 + Val2);
    }

    [Chip("CharSubtract")]
    [Serializable]
    public sealed class Subtract : BF
    {
        public override char Func(char Val1, char Val2) => (char)(Val1 - Val2);
    }

    [Chip("CharMultiply")]
    [Serializable]
    public sealed class Multiply : BF
    {
        public override char Func(char Val1, char Val2) => (char)(Val1 * Val2);
    }

    [Chip("CharDivide")]
    [Serializable]
    public sealed class Divide : BF
    {
        public override char Func(char Val1, char Val2) => (char)(Val1 / Val2);
    }

    [Chip("CharModulus")]
    [Serializable]
    public sealed class Modulus : BF
    {
        public override char Func(char Val1, char Val2) => (char)(Val1 % Val2);
    }

    [Chip("CharClamp")]
    [Serializable]
    public sealed class Clamp : Clamp<char>
    {
    }
}