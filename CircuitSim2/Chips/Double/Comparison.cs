using BF = CircuitSim2.Chips.Functors.BinaryFunctor<double, double, bool>;

namespace CircuitSim2.Chips.Double.Comparison
{
    public sealed class Equal : BF
    {
        public Equal(Engine.Engine Engine = null) : base("DoubleEqual", (a, b) => a == b, Engine)
        {

        }
    }

    public sealed class NotEqual : BF
    {
        public NotEqual(Engine.Engine Engine = null) : base("DoubleNotEqual", (a, b) => a != b, Engine)
        {

        }
    }

    public sealed class LessThan : BF
    {
        public LessThan(Engine.Engine Engine = null) : base("DoubleLessThan", (a, b) => a < b, Engine)
        {

        }
    }

    public sealed class LessThanEqual : BF
    {
        public LessThanEqual(Engine.Engine Engine = null) : base("DoubleLessThanEqual", (a, b) => a <= b, Engine)
        {

        }
    }

    public sealed class GreaterThan : BF
    {
        public GreaterThan(Engine.Engine Engine = null) : base("DoubleGreaterThan", (a, b) => a > b, Engine)
        {

        }
    }

    public sealed class GreaterThanEqual : BF
    {
        public GreaterThanEqual(Engine.Engine Engine = null) : base("DoubleGreaterThanEqual", (a, b) => a >= b, Engine)
        {

        }
    }
}
