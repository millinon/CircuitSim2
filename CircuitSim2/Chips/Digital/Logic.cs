using UF = CircuitSim2.Chips.Functors.UnaryFunctor<bool, bool>;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<bool, bool, bool>;

namespace CircuitSim2.Chips.Digital.Logic
{
    [Chip("DigitalNOT")]
    public sealed class NOT : UF
    {
        private NOT(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public NOT(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public NOT(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public NOT() : this(null, null)
        {
        }

        public override bool Func(bool Value) => !Value;
    }

    [Chip("DigitalAND")]
    public sealed class AND : BF
    {
        private AND(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public AND(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public AND(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public AND() : this(null, null)
        {
        }

        public override bool Func(bool Val1, bool Val2) => Val1 && Val2;
    }

    [Chip("DigitalOR")]
    public sealed class OR : BF
    {
        private OR(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public OR(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public OR(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public OR() : this(null, null)
        {
        }

        public override bool Func(bool Val1, bool Val2) => Val1 || Val2;
    }

    [Chip("DigitalXOR")]
    public sealed class XOR : BF
    {
        private XOR(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public XOR(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public XOR(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public XOR() : this(null, null)
        {
        }

        public override bool Func(bool Val1, bool Val2) => Val1 ^ Val2;
    }

    [Chip("DigitalNAND")]
    public sealed class NAND : BF
    {
        private NAND(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public NAND(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public NAND(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public NAND() : this(null, null)
        {
        }

        public override bool Func(bool Val1, bool Val2) => !(Val1 && Val2);
    }

    [Chip("DigitalNOR")]
    public sealed class NOR : BF
    {
        private NOR(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public NOR(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public NOR(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public NOR() : this(null, null)
        {
        }

        public override bool Func(bool Val1, bool Val2) => !(Val1 || Val2);
    }
}
