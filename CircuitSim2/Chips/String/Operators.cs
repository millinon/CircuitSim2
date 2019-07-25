using System.Linq;

using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.String.Operators
{
    [Chip("StringLength")]
    public sealed class Length : UnaryFunctor<string, int>
    {
        public Length(Engine.Engine Engine = null) : base(a => a.Length, Engine)
        {

        }
    }

    [Chip("StringCharAt")]
    public sealed class CharAt : BinaryFunctor<string, int, char>
    {
        public CharAt(Engine.Engine Engine = null) : base((a, b) => a[b], Engine)
        {

        }
    }

    [Chip("StringSubstring")]
    public sealed class Substring : BinaryFunctor<string, int, string>
    {
        public Substring(Engine.Engine Engine = null) : base((a, b) => a.Substring(b), Engine)
        {

        }
    }

    [Chip("StringIndexOf")]
    public sealed class IndexOf : BinaryFunctor<string, string, int>
    {
        public IndexOf(Engine.Engine Engine = null) : base((a, b) => a.IndexOf(b), Engine)
        {

        }
    }

    [Chip("StringConcat")]
    public sealed class Concat : BinaryFunctor<string, string, string>
    {
        public Concat(Engine.Engine Engine = null) : base((a, b) => a + b, Engine)
        {

        }
    }

    [Chip("StringAppend")]
    public sealed class Append : BinaryFunctor<string, char, string>
    {
        public Append(Engine.Engine Engine = null) : base((a, b) => a + b, Engine)
        {

        }
    }

    [Chip("StringToLower")]
    public sealed class ToLower : UnaryFunctor<string, string>
    {
        public ToLower(Engine.Engine Engine = null) : base(a => a.ToLower(), Engine)
        {

        }
    }

    [Chip("StringToUpper")]
    public sealed class ToUpper : UnaryFunctor<string, string>
    {
        public ToUpper(Engine.Engine Engine = null) : base(a => a.ToUpper(), Engine)
        {

        }
    }

    [Chip("StringReverse")]
    public sealed class Reverse : UnaryFunctor<string, string>
    {
        public Reverse(Engine.Engine Engine = null) : base(a => a.Reverse().ToString(), Engine)
        {

        }
    }
}
