using BF = CircuitSim2.Chips.Functors.BinaryFunctor<long, long, bool>;

namespace CircuitSim2.Chips.Long.Comparison
{
    public sealed class Equal : BF
    {
        public Equal(Engine.Engine Engine = null) : base("LongEqual", (a, b) => a == b, Engine)
        {

        }
    }

    public sealed class NotEqual : BF
    {
        public NotEqual(Engine.Engine Engine = null) : base("LongNotEqual", (a, b) => a != b, Engine)
        {

        }
    }

    public sealed class LessThan : BF
    {
        public LessThan(Engine.Engine Engine = null) : base("LongLessThan", (a, b) => a < b, Engine)
        {

        }
    }

    public sealed class LessThanEqual : BF
    {
        public LessThanEqual(Engine.Engine Engine = null) : base("LongLessThanEqual", (a, b) => a <= b, Engine)
        {

        }
    }

    public sealed class GreaterThan : BF
    {
        public GreaterThan(Engine.Engine Engine = null) : base("LongGreaterThan", (a, b) => a > b, Engine)
        {

        }
    }

    public sealed class GreaterThanEqual : BF
    {
        public GreaterThanEqual(Engine.Engine Engine = null) : base("LongGreaterThanEqual", (a, b) => a >= b, Engine)
        {

        }
    }
}
