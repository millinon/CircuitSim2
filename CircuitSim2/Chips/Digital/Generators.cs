using G = CircuitSim2.Chips.Functors.Generator<bool>;

namespace CircuitSim2.Chips.Digital.Generators
{
    [Chip("DigitalRandom")]
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

        protected sealed override bool NextValue() => Generator.NextDouble() <= 0.5;
    }
}
