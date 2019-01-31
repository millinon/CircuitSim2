using BF = CircuitSim2.Chips.Functors.BinaryFunctor<char, char, char>;

namespace CircuitSim2.Chips.Char.Arithmetic
{
    public sealed class Add : BF
    {
        public Add(Engine.Engine Engine = null) : base("CharAdd", (a, b) => (char)(a + b), Engine)
        {

        }
    }

    public sealed class Subtract : BF
    {
        public Subtract(Engine.Engine Engine = null) : base("CharSubtract", (a, b) => (char)(a - b), Engine)
        {

        }
    }

    public sealed class Multiply : BF
    {
        public Multiply(Engine.Engine Engine = null) : base("CharMultiply", (a, b) => (char)(a * b), Engine)
        {

        }
    }

    public sealed class Divide : BF
    {
        public Divide(Engine.Engine Engine = null) : base("CharDivide", (a, b) => (char)(a / b), Engine)
        {

        }
    }

    public sealed class Modulus : BF
    {
        public Modulus(Engine.Engine Engine = null) : base("CharModulus", (a, b) => (char)(a % b), Engine)
        {

        }
    }

    public sealed class LeftShift : BF
    {
        public LeftShift(Engine.Engine Engine = null) : base("CharLeftShift", (a, b) => (char)(a << b), Engine)
        {

        }
    }

    public sealed class RightShift : BF
    {
        public RightShift(Engine.Engine Engine = null) : base("CharRightShift", (a, b) => (char)(a >> b), Engine)
        {

        }
    }
}
