using BF = CircuitSim2.Chips.Functors.BinaryFunctor<char, char, char>;

namespace CircuitSim2.Chips.Char.Arithmetic
{
    [Chip("CharAdd")]
    public sealed class Add : BF
    {
        private Add(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Add(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Add(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public Add() : this(null, null)
        {
        }

        public override char Func(char Val1, char Val2) => (char)(Val1 + Val2);
    }

    [Chip("CharSubtract")]
    public sealed class Subtract : BF
    {
        private Subtract(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Subtract(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Subtract(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public Subtract() : this(null, null)
        {
        }

        public override char Func(char Val1, char Val2) => (char)(Val1 - Val2);
    }

    [Chip("CharMultiply")]
    public sealed class Multiply : BF
    {
        private Multiply(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Multiply(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Multiply(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public Multiply() : this(null, null)
        {
        }

        public override char Func(char Val1, char Val2) => (char)(Val1 * Val2);
    }

    [Chip("CharDivide")]
    public sealed class Divide : BF
    {
        private Divide(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Divide(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Divide(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public Divide() : this(null, null)
        {
        }

        public override char Func(char Val1, char Val2) => (char)(Val1 / Val2);
    }

    [Chip("CharModulus")]
    public sealed class Modulus : BF
    {
        private Modulus(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Modulus(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Modulus(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public Modulus() : this(null, null)
        {
        }

        public override char Func(char Val1, char Val2) => (char)(Val1 % Val2);
    }
}