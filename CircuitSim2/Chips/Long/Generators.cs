using G = CircuitSim2.Chips.Functors.Generator<long>;

namespace CircuitSim2.Chips.Long.Generators
{
    [Chip("LongRandom")]
    public sealed class Random : G
    {
        private readonly System.Random Generator;

        private Random(int Seed, ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine) => Generator = new System.Random(Seed);

        public Random(int Seed, ChipBase ParentChip) : this(Seed, ParentChip, ParentChip?.Engine)
        {
        }

        public Random(int Seed, Engine.Engine Engine) : this(Seed, null, Engine)
        {
        }

        public Random(int Seed = 1337) : this(Seed, null, null)
        {
        }

        protected sealed override long NextValue() => ((long)Generator.Next() << 32) + Generator.Next();
    }
}
