using UF = CircuitSim2.Chips.Functors.UnaryFunctor<bool, bool>;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<bool, bool, bool>;

namespace CircuitSim2.Chips.Digital.Logic
{
    [PureChip("DigitalNOT")]
    public sealed class NOT : UF
    {
        public NOT(Engine.Engine Engine = null) : base(a => !a)
        {

        }
    }

    [PureChip("DigitalAND")]
    public sealed class AND : BF
    {
        public AND(Engine.Engine Engine = null) : base((a, b) => a && b)
        {

        }
    }

    [PureChip("DigitalOR")]
    public sealed class OR : BF
    {
        public OR(Engine.Engine Engine = null) : base((a, b) => a || b)
        {

        }
    }

    [PureChip("DigitalXOR")]
    public sealed class XOR : BF
    {
        public XOR(Engine.Engine Engine = null) : base((a, b) => a ^ b)
        {

        }
    }

    [PureChip("DigitalNAND")]
    public sealed class NAND : BF
    {
        public NAND(Engine.Engine Engine = null) : base((a, b) => !(a && b))
        {

        }
    }

    [PureChip("DigitalNOR")]
    public sealed class NOR : BF
    {
        public NOR(Engine.Engine Engine = null) : base((a, b) => !(a || b))
        {

        }
    }
}
