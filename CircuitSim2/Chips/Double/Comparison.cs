using BF = CircuitSim2.Chips.Functors.BinaryFunctor<double, double, bool>;

namespace CircuitSim2.Chips.Double.Comparison
{
    [PureChip("DoubleEqual")]
    public sealed class Equal : BF
    {
        public Equal(Engine.Engine Engine = null) : base((a, b) => a == b, Engine)
        {

        }
    }

    [PureChip("DoubleNotEqual")]
    public sealed class NotEqual : BF
    {
        public NotEqual(Engine.Engine Engine = null) : base((a, b) => a != b, Engine)
        {

        }
    }

    [PureChip("DoubleLessThan")]
    public sealed class LessThan : BF
    {
        public LessThan(Engine.Engine Engine = null) : base((a, b) => a < b, Engine)
        {

        }
    }

    [PureChip("DoubleLessThanEqual")]
    public sealed class LessThanEqual : BF
    {
        public LessThanEqual(Engine.Engine Engine = null) : base((a, b) => a <= b, Engine)
        {

        }
    }

    [PureChip("DoubleGreaterThan")]
    public sealed class GreaterThan : BF
    {
        public GreaterThan(Engine.Engine Engine = null) : base((a, b) => a > b, Engine)
        {

        }
    }

    [PureChip("DoubleGreaterThanEqual")]
    public sealed class GreaterThanEqual : BF
    {
        public GreaterThanEqual(Engine.Engine Engine = null) : base((a, b) => a >= b, Engine)
        {

        }
    }
}
