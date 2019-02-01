using BF = CircuitSim2.Chips.Functors.BinaryFunctor<string, string, bool>;

namespace CircuitSim2.Chips.String.Comparison
{
    [PureChip("StringEqual")]
    public sealed class Equal : BF
    {
        public Equal(Engine.Engine Engine = null) : base((a, b) => a == b, Engine)
        {

        }
    }

    [PureChip("StringNotEqual")]
    public sealed class NotEqual : BF
    {
        public NotEqual(Engine.Engine Engine = null) : base((a, b) => a != b, Engine)
        {

        }
    }
}
