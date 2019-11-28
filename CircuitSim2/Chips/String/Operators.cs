using System;
using System.Linq;

using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.String.Operators
{
    [Chip("StringLength")]
    [Serializable]
    public sealed class Length : UnaryFunctor<string, int>
    {
        public override int Func(string Value) => Value.Length;
    }

    [Chip("StringCharAt")]
    [Serializable]
    public sealed class CharAt : BinaryFunctor<string, int, char>
    {
        public override char Func(string Val1, int Val2) => Val1[Val2];
    }

    [Chip("StringSubstring")]
    [Serializable]
    public sealed class Substring : BinaryFunctor<string, int, string>
    {
        public override string Func(string Val1, int Val2) => Val1.Substring(Val2);
    }

    [Chip("StringIndexOf")]
    [Serializable]
    public sealed class IndexOf : BinaryFunctor<string, string, int>
    {
        public override int Func(string Val1, string Val2) => Val1.IndexOf(Val2);
    }

    [Chip("StringConcat")]
    [Serializable]
    public sealed class Concat : BinaryFunctor<string, string, string>
    {

        public override string Func(string Val1, string Val2) => Val1 + Val2;
    }

    [Chip("StringAppend")]
    [Serializable]
    public sealed class Append : BinaryFunctor<string, char, string>
    {
        public override string Func(string Val1, char Val2) => Val1 + Val2;
    }

    [Chip("StringToLower")]
    [Serializable]
    public sealed class ToLower : UnaryFunctor<string, string>
    {
        public override string Func(string Value) => Value.ToLower();
    }

    [Chip("StringToUpper")]
    [Serializable]
    public sealed class ToUpper : UnaryFunctor<string, string>
    {
        public override string Func(string Value) => Value.ToUpper();
    }

    [Chip("StringReverse")]
    [Serializable]
    public sealed class Reverse : UnaryFunctor<string, string>
    {
        public override string Func(string Value) => string.Concat(Value.Reverse());
    }
}
