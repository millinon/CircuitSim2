using BF = CircuitSim2.Chips.Functors.BinaryFunctor<char, char, char>;

namespace CircuitSim2.Chips.Char.Arithmetic
{
    [Chip("CharAdd")]
    public sealed class Add : BF
    {
        public Add(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Add(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Add(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override char Func(char Val1, char Val2) => (char)(Val1 + Val2);
    }

    [Chip("CharSubtract")]
    public sealed class Subtract : BF
    {
        public Subtract(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Subtract(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Subtract(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override char Func(char Val1, char Val2) => (char)(Val1 - Val2);
    }

    [Chip("CharMultiply")]
    public sealed class Multiply : BF
    {
        public Multiply(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Multiply(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Multiply(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override char Func(char Val1, char Val2) => (char)(Val1 * Val2);
    }

    [Chip("CharDivide")]
    public sealed class Divide : BF
    {
        public Divide(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Divide(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Divide(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override char Func(char Val1, char Val2) => (char)(Val1 / Val2);
    }

    [Chip("CharModulus")]
    public sealed class Modulus : BF
    {
        public Modulus(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Modulus(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Modulus(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override char Func(char Val1, char Val2) => (char)(Val1 % Val2);
    }
}