using BF = CircuitSim2.Chips.Functors.BinaryFunctor<char, char, char>;

namespace CircuitSim2.Chips.Char.Arithmetic
{
    [Chip("CharAdd")]
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base((a, b) => (char)(a + b), Engine)
        {

        }
    }

    [Chip("CharSubtract")]
    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base((a, b) => (char)(a - b), Engine)
        {

        }
    }

    [Chip("CharMultiply")]
    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base((a, b) => (char)(a * b), Engine)
        {

        }
    }

    [Chip("CharDivide")]
    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base((a, b) => (char)(a / b), Engine)
        {

        }
    }

    [Chip("CharModulus")]
    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base((a, b) => (char)(a % b), Engine)
        {

        }
    }

    [Chip("CharLeftShift")]
    public sealed class LeftShift : BF
    {
        public LeftShift(Engine.Engine Engine = null) : base((a, b) => (char)(a << b), Engine)
        {

        }
    }

    [Chip("CharRightShift")]
    public sealed class RightShift : BF
    {
        public RightShift(Engine.Engine Engine = null) : base((a, b) => (char)(a >> b), Engine)
        {

        }
    }
}
