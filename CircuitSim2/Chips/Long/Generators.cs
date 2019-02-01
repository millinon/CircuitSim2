using G = CircuitSim2.Chips.Functors.Generator<long>;

namespace CircuitSim2.Chips.Long.Generators
{
    [Chip("LongRandom")]
    public sealed class Random : G
    {
        private readonly System.Random Generator;

        public Random(Engine.Engine Engine = null) : this(1337, Engine)
        {

        }

        public Random(int Seed = -1, Engine.Engine Engine = null) : base(Engine) => Generator = new System.Random(Seed);

        protected sealed override long NextValue() => ((long)Generator.Next() << 32) + Generator.Next();
    }
}
