using BF = CircuitSim2.Chips.Functors.BinaryFunctor<char, char, bool>;

namespace CircuitSim2.Chips.Char.Comparison
{
    public sealed class Equal : BF
    {
        public Equal(Engine.Engine Engine = null) : base("CharEqual", (a, b) => a == b, Engine)
        {

        }
    }

    public sealed class NotEqual : BF
    {
        public NotEqual(Engine.Engine Engine = null) : base("CharNotEqual", (a, b) => a != b, Engine)
        {

        }
    }

    public sealed class LessThan : BF
    {
        public LessThan(Engine.Engine Engine = null) : base("CharLessThan", (a, b) => a < b, Engine)
        {

        }
    }

    public sealed class LessThanEqual : BF
    {
        public LessThanEqual(Engine.Engine Engine = null) : base("CharLessThanEqual", (a, b) => a <= b, Engine)
        {

        }
    }

    public sealed class GreaterThan : BF
    {
        public GreaterThan(Engine.Engine Engine = null) : base("CharGreaterThan", (a, b) => a > b, Engine)
        {

        }
    }

    public sealed class GreaterThanEqual : BF
    {
        public GreaterThanEqual(Engine.Engine Engine = null) : base("CharGreaterThanEqual", (a, b) => a >= b, Engine)
        {

        }
    }
}
