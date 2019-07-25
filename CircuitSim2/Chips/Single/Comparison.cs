using BF = CircuitSim2.Chips.Functors.BinaryFunctor<float, float, bool>;

namespace CircuitSim2.Chips.Single.Comparison
{
    [Chip("SingleEqual")]
    public sealed class Equal : BF
    {
        public Equal(Engine.Engine Engine = null) : base((a, b) => a == b, Engine)
        {

        }
    }

    [Chip("SingleNotEqual")]
    public sealed class NotEqual : BF
    {
        public NotEqual(Engine.Engine Engine = null) : base((a, b) => a != b, Engine)
        {

        }
    }

    [Chip("SingleLessThan")]
    public sealed class LessThan : BF
    {
        public LessThan(Engine.Engine Engine = null) : base((a, b) => a < b, Engine)
        {

        }
    }

    [Chip("SingleLessThanEqual")]
    public sealed class LessThanEqual : BF
    {
        public LessThanEqual(Engine.Engine Engine = null) : base((a, b) => a <= b, Engine)
        {

        }
    }

    [Chip("SingleGreaterThan")]
    public sealed class GreaterThan : BF
    {
        public GreaterThan(Engine.Engine Engine = null) : base((a, b) => a > b, Engine)
        {

        }
    }

    [Chip("SingleGreaterThanEqual")]
    public sealed class GreaterThanEqual : BF
    {
        public GreaterThanEqual(Engine.Engine Engine = null) : base((a, b) => a >= b, Engine)
        {

        }
    }
}
