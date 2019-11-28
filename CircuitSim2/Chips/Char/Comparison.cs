using System;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<char, char, bool>;

namespace CircuitSim2.Chips.Char.Comparison
{
    [Chip("CharEqual")]
    [Serializable]
    public sealed class Equal : BF
    {
        public override bool Func(char Val1, char Val2) => Val1 == Val2;
    }

    [Chip("CharNotEqual")]
    [Serializable]
    public sealed class NotEqual : BF
    {
        public override bool Func(char Val1, char Val2) => Val1 != Val2;
    }

    [Chip("CharLessThan")]
    [Serializable]
    public sealed class LessThan : BF
    {
        public override bool Func(char Val1, char Val2) => Val1 < Val2;
    }

    [Chip("CharLessThanEqual")]
    [Serializable]
    public sealed class LessThanEqual : BF
    {
        public override bool Func(char Val1, char Val2) => Val1 <= Val2;
    }

    [Chip("CharGreaterThan")]
    [Serializable]
    public sealed class GreaterThan : BF
    {
        public override bool Func(char Val1, char Val2) => Val1 > Val2;
    }

    [Chip("CharGreaterThanEqual")]
    [Serializable]
    public sealed class GreaterThanEqual : BF
    {
        public override bool Func(char Val1, char Val2) => Val1 >= Val2;
    }
}
