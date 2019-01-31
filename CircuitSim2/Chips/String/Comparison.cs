using BF = CircuitSim2.Chips.Functors.BinaryFunctor<string, string, bool>;

namespace CircuitSim2.Chips.String.Comparison
{
    public sealed class Equal : BF
    {
        public Equal(Engine.Engine Engine = null) : base("StringEqual", (a, b) => a == b, Engine)
        {

        }
    }

    public sealed class NotEqual : BF
    {
        public NotEqual(Engine.Engine Engine = null) : base("StringNotEqual", (a, b) => a != b, Engine)
        {

        }
    }
}
