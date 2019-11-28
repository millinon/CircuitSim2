using CircuitSim2.Chips.Functors;
using System;
using R = CircuitSim2.Chips.Functors.Random<byte>;

namespace CircuitSim2.Chips.Byte.Generators
{
    [Chip("ByteRandom")]
    [Serializable]
    public sealed class Random : R
    {

        [NonSerialized]
        private readonly byte[] buf = new byte[1];

        protected sealed override byte NextValue()
        {
            RNG.NextBytes(buf);
            return buf[0];
        }
    }

    [Chip("ByteConstant")]
    [Serializable]
    public sealed class Constant : Constant<byte>
    {
    }
}
