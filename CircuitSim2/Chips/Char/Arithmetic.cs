using BF = CircuitSim2.Chips.Functors.BinaryFunctor<char, char, char>;

namespace CircuitSim2.Chips.Char.Arithmetic
{
    [PureChip("CharAdd")]
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base((a, b) => (char)(a + b), Engine)
        {

        }
    }

    [PureChip("CharSubtract")]
    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base((a, b) => (char)(a - b), Engine)
        {

        }
    }

    [PureChip("CharMultiply")]
    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base((a, b) => (char)(a * b), Engine)
        {

        }
    }

    [PureChip("CharDivide")]
    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base((a, b) => (char)(a / b), Engine)
        {

        }
    }

    [PureChip("CharModulus")]
    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base((a, b) => (char)(a % b), Engine)
        {

        }
    }

    [PureChip("CharLeftShift")]
    public sealed class LeftShift : BF
    {
        public LeftShift(Engine.Engine Engine = null) : base((a, b) => (char)(a << b), Engine)
        {

        }
    }

    [PureChip("CharRightShift")]
    public sealed class RightShift : BF
    {
        public RightShift(Engine.Engine Engine = null) : base((a, b) => (char)(a >> b), Engine)
        {

        }
    }
}
