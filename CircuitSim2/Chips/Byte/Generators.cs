using CircuitSim2.Chips.Functors;
using R = CircuitSim2.Chips.Functors.Random<byte>;

namespace CircuitSim2.Chips.Byte.Generators
{
    [Chip("ByteRandom")]
    public sealed class Random : R
    {
        public Random(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Random(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Random(Engine.Engine Engine) : this(null, Engine)
        {
        }

        private readonly byte[] buf = new byte[1];

        protected sealed override byte NextValue()
        {
            RNG.NextBytes(buf);
            return buf[0];
        }
    }

    [Chip("ByteConstant")]
    public sealed class Constant : Constant<byte>
    {
        public Constant(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public Constant(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Constant(Engine.Engine Engine) : this(null, Engine)
        {
        }
    }
}
