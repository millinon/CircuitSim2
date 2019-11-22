using System.Linq;

using CircuitSim2.Chips.Functors;
using CircuitSim2.Engine;

namespace CircuitSim2.Chips.String.Operators
{
    [Chip("StringLength")]
    public sealed class Length : UnaryFunctor<string, int>
    {
        public Length(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Length(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Length(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override int Func(string Value) => Value.Length;
    }

    [Chip("StringCharAt")]
    public sealed class CharAt : BinaryFunctor<string, int, char>
    {
        public CharAt(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public CharAt(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public CharAt(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override char Func(string Val1, int Val2) => Val1[Val2];
    }

    [Chip("StringSubstring")]
    public sealed class Substring : BinaryFunctor<string, int, string>
    {
        public Substring(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Substring(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Substring(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override string Func(string Val1, int Val2) => Val1.Substring(Val2);
    }

    [Chip("StringIndexOf")]
    public sealed class IndexOf : BinaryFunctor<string, string, int>
    {
        public IndexOf(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public IndexOf(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public IndexOf(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override int Func(string Val1, string Val2) => Val1.IndexOf(Val2);
    }

    [Chip("StringConcat")]
    public sealed class Concat : BinaryFunctor<string, string, string>
    {
        public Concat(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Concat(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Concat(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override string Func(string Val1, string Val2) => Val1 + Val2;
    }

    [Chip("StringAppend")]
    public sealed class Append : BinaryFunctor<string, char, string>
    {
        public Append(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Append(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Append(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override string Func(string Val1, char Val2) => Val1 + Val2;
    }

    [Chip("StringToLower")]
    public sealed class ToLower : UnaryFunctor<string, string>
    {
        public ToLower(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToLower(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToLower(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override string Func(string Value) => Value.ToLower();
    }

    [Chip("StringToUpper")]
    public sealed class ToUpper : UnaryFunctor<string, string>
    {
        public ToUpper(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToUpper(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToUpper(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override string Func(string Value) => Value.ToUpper();
    }

    [Chip("StringReverse")]
    public sealed class Reverse : UnaryFunctor<string, string>
    {
        public Reverse(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Reverse(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Reverse(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override string Func(string Value) => string.Concat(Value.Reverse());
    }
}
