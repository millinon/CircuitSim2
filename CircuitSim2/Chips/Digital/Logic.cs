using UF = CircuitSim2.Chips.Functors.UnaryFunctor<bool, bool>;
using BF = CircuitSim2.Chips.Functors.BinaryFunctor<bool, bool, bool>;
using System;

namespace CircuitSim2.Chips.Digital.Logic
{
    [Chip("DigitalNOT")]
    [Serializable]
    public sealed class NOT : UF
    {
        public override bool Func(bool Value) => !Value;
    }

    [Chip("DigitalAND")]
    [Serializable]
    public sealed class AND : BF
    {
        public override bool Func(bool Val1, bool Val2) => Val1 && Val2;
    }

    [Chip("DigitalOR")]
    [Serializable]
    public sealed class OR : BF
    {

        public override bool Func(bool Val1, bool Val2) => Val1 || Val2;
    }

    [Chip("DigitalXOR")]
    [Serializable]
    public sealed class XOR : BF
    {
        public override bool Func(bool Val1, bool Val2) => Val1 ^ Val2;
    }

    [Chip("DigitalNAND")]
    [Serializable]
    public sealed class NAND : BF
    {
        public override bool Func(bool Val1, bool Val2) => !(Val1 && Val2);
    }

    [Chip("DigitalNOR")]
    [Serializable]
    public sealed class NOR : BF
    {
        public override bool Func(bool Val1, bool Val2) => !(Val1 || Val2);
    }
}
