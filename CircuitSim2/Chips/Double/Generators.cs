using G = CircuitSim2.Chips.Functors.Generator<double>;

namespace CircuitSim2.Chips.Double.Generators
{
    [Chip("DoubleRandom")]
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

        protected sealed override double NextValue() => Generator.NextDouble();
    }
}