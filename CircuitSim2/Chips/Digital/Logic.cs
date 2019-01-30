using UF = CircuitSim2.Chips.Functors.UnaryFunctor<bool, bool>;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<bool, bool, bool>;

namespace CircuitSim2.Chips.Digital.Logic
{
    public sealed class NOT : UF
    {
        public NOT(Engine.Engine Engine = null) : base("NOT", a => !a)
        {

        }
    }

    public sealed class AND : BF
    {
        public AND(Engine.Engine Engine = null) : base("AND", (a, b) => a && b)
        {

        }
    }

    public sealed class OR : BF
    {
        public OR(Engine.Engine Engine = null) : base("OR", (a, b) => a || b)
        {

        }
    }
    
    public sealed class XOR : BF
    {
        public XOR(Engine.Engine Engine = null) : base("XOR", (a, b) => a ^ b)
        {

        }
    }

    public sealed class NAND : BF
    {
        public NAND(Engine.Engine Engine = null) : base("NAND", (a, b) => !(a && b))
        {

        }
    }

    public sealed class NOR : BF
    {
        public NOR(Engine.Engine Engine = null) : base("NOR", (a, b) => !(a || b))
        {

        }
    }
}
