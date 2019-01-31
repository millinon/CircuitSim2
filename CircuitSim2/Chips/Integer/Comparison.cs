using BF = CircuitSim2.Chips.Functors.BinaryFunctor<int, int, bool>;

namespace CircuitSim2.Chips.Integer.Comparison
{
    public sealed class Equal : BF
    {
        public Equal(Engine.Engine Engine = null) : base("IntegerEqual", (a, b) => a == b, Engine)
        {

        }
    }

    public sealed class NotEqual : BF
    {
        public NotEqual(Engine.Engine Engine = null) : base("IntegerNotEqual", (a, b) => a != b, Engine)
        {

        }
    }

    public sealed class LessThan : BF
    {
        public LessThan(Engine.Engine Engine = null) : base("IntegerLessThan", (a, b) => a < b, Engine)
        {

        }
    }

    public sealed class LessThanEqual : BF
    {
        public LessThanEqual(Engine.Engine Engine = null) : base("IntegerLessThanEqual", (a, b) => a <= b, Engine)
        {

        }
    }

    public sealed class GreaterThan : BF
    {
        public GreaterThan(Engine.Engine Engine = null) : base("IntegerGreaterThan", (a, b) => a > b, Engine)
        {

        }
    }

    public sealed class GreaterThanEqual : BF
    {
        public GreaterThanEqual(Engine.Engine Engine = null) : base("IntegerGreaterThanEqual", (a, b) => a >= b, Engine)
        {

        }
    }
}
