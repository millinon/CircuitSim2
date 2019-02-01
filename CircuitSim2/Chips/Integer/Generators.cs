using G = CircuitSim2.Chips.Functors.Generator<int>;

namespace CircuitSim2.Chips.Integer.Generators
{
    [Chip("IntegerRandom")]
    public sealed class Random : G
    {
        private readonly System.Random Generator;

        public Random(Engine.Engine Engine = null) : this(1337, Engine)
        {

        }

        public Random(int Seed = -1, Engine.Engine Engine = null) : base(Engine) => Generator = new System.Random(Seed);

        protected sealed override int NextValue() => Generator.Next();
    }
}
