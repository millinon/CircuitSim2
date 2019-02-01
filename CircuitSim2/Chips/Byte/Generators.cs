using G = CircuitSim2.Chips.Functors.Generator<byte>;

namespace CircuitSim2.Chips.Byte.Generators
{
    [Chip("ByteRandom")]
    public sealed class Random : G
    {
        private readonly System.Random Generator;

        public Random(Engine.Engine Engine = null) : this(1337, Engine)
        {

        }

        public Random(int Seed = -1, Engine.Engine Engine = null) : base(Engine) => Generator = new System.Random(Seed);

        private byte[] buf = new byte[1];

        protected sealed override byte NextValue()
        {
            Generator.NextBytes(buf);
            return buf[0];
        }
    }
}
