using BF = CircuitSim2.Chips.Functors.BinaryFunctor<bool, bool, bool>;

namespace CircuitSim2.Chips.Digital.Comparison
{
    [PureChip("DigitalEqual")]
    public sealed class Equal : BF
    {
        public Equal(Engine.Engine Engine = null) : base((a, b) => a == b, Engine)
        {

        }
    }

    [PureChip("DigitalNotEqual")]
    public sealed class NotEqual : BF
    {
        public NotEqual(Engine.Engine Engine = null) : base((a, b) => a != b, Engine)
        {

        }
    }
}
