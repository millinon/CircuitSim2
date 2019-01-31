using G = CircuitSim2.Chips.Functors.Generator<double>;

namespace CircuitSim2.Chips.Double.Generators
{
    public sealed class Random : G
    {
        private readonly System.Random Generator;
        
        public Random(Engine.Engine Engine = null) : this(1337, Engine)
        {

        }

        public Random(int Seed = -1, Engine.Engine Engine = null) : base("DoubleRandom", Engine) => Generator = new System.Random(Seed);

        protected sealed override double NextValue() => Generator.NextDouble();
    }
}
