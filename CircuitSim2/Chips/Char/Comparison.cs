using BF = CircuitSim2.Chips.Functors.BinaryFunctor<char, char, bool>;

namespace CircuitSim2.Chips.Char.Comparison
{
    [Chip("CharEqual")]
    public sealed class Equal : BF
    {
        public Equal(Engine.Engine Engine = null) : base((a, b) => a == b, Engine)
        {

        }
    }

    [Chip("CharNotEqual")]
    public sealed class NotEqual : BF
    {
        public NotEqual(Engine.Engine Engine = null) : base((a, b) => a != b, Engine)
        {

        }
    }

    [Chip("CharLessThan")]
    public sealed class LessThan : BF
    {
        public LessThan(Engine.Engine Engine = null) : base((a, b) => a < b, Engine)
        {

        }
    }

    [Chip("CharLessThanEqual")]
    public sealed class LessThanEqual : BF
    {
        public LessThanEqual(Engine.Engine Engine = null) : base((a, b) => a <= b, Engine)
        {

        }
    }

    [Chip("CharGreaterThan")]
    public sealed class GreaterThan : BF
    {
        public GreaterThan(Engine.Engine Engine = null) : base((a, b) => a > b, Engine)
        {

        }
    }

    [Chip("CharGreaterThanEqual")]
    public sealed class GreaterThanEqual : BF
    {
        public GreaterThanEqual(Engine.Engine Engine = null) : base((a, b) => a >= b, Engine)
        {

        }
    }
}
