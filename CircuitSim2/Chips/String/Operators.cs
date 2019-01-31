using System.Linq;

using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.String.Operators
{
    public sealed class Length : UnaryFunctor<string, int>
    {
        public Length(Engine.Engine Engine = null) : base("StringLength", a => a.Length, Engine)
        {

        }
    }

    public sealed class CharAt : BinaryFunctor<string, int, char>
    {
        public CharAt(Engine.Engine Engine = null) : base("StringCharAt", (a, b) => a[b], Engine)
        {

        }
    }

    public sealed class Substring : BinaryFunctor<string, int, string>
    {
        public Substring(Engine.Engine Engine = null) : base("StringSubstring", (a, b) => a.Substring(b), Engine)
        {

        }
    }

    public sealed class IndexOf : BinaryFunctor<string, string, int>
    {
        public IndexOf(Engine.Engine Engine = null) : base("StringIndexOf", (a, b) => a.IndexOf(b), Engine)
        {

        }
    }

    public sealed class Concat : BinaryFunctor<string, string, string>
    {
        public Concat(Engine.Engine Engine = null) : base("StringConcat", (a, b) => a + b, Engine)
        {

        }
    }

    public sealed class Append : BinaryFunctor<string, char, string>
    {
        public Append(Engine.Engine Engine = null) : base("StringAppend", (a, b) => a + b, Engine)
        {

        }
    }

    public sealed class ToLower : UnaryFunctor<string, string>
    {
        public ToLower(Engine.Engine Engine = null) : base("StringToLower", a => a.ToLower(), Engine)
        {

        }
    }

    public sealed class ToUpper : UnaryFunctor<string, string>
    {
        public ToUpper(Engine.Engine Engine = null) : base("StringToUpper", a => a.ToUpper(), Engine)
        {

        }
    }

    public sealed class Reverse : UnaryFunctor<string, string>
    {
        public Reverse(Engine.Engine Engine = null) : base("StringReverse", a => a.Reverse().ToString(), Engine)
        {

        }
    }
}
