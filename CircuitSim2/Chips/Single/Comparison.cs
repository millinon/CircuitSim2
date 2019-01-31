using BF = CircuitSim2.Chips.Functors.BinaryFunctor<float, float, bool>;

namespace CircuitSim2.Chips.Single.Comparison
{
    public sealed class Equal : BF
    {
        public Equal(Engine.Engine Engine = null) : base("SingleEqual", (a, b) => a == b, Engine)
        {

        }
    }

    public sealed class NotEqual : BF
    {
        public NotEqual(Engine.Engine Engine = null) : base("SingleNotEqual", (a, b) => a != b, Engine)
        {

        }
    }

    public sealed class LessThan : BF
    {
        public LessThan(Engine.Engine Engine = null) : base("SingleLessThan", (a, b) => a < b, Engine)
        {

        }
    }

    public sealed class LessThanEqual : BF
    {
        public LessThanEqual(Engine.Engine Engine = null) : base("SingleLessThanEqual", (a, b) => a <= b, Engine)
        {

        }
    }

    public sealed class GreaterThan : BF
    {
        public GreaterThan(Engine.Engine Engine = null) : base("SingleGreaterThan", (a, b) => a > b, Engine)
        {

        }
    }

    public sealed class GreaterThanEqual : BF
    {
        public GreaterThanEqual(Engine.Engine Engine = null) : base("SingleGreaterThanEqual", (a, b) => a >= b, Engine)
        {

        }
    }
}
