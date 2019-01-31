using G = CircuitSim2.Chips.Functors.Generator<bool>;

namespace CircuitSim2.Chips.Digital.Generators
{
    public sealed class Random : G
    {
        private readonly System.Random Generator;
        
        public Random(Engine.Engine Engine = null) : this(1337, Engine)
        {

        }

        public Random(int Seed = -1, Engine.Engine Engine = null) : base("DigitalRandom", Engine) => Generator = new System.Random(Seed);

        protected sealed override bool NextValue() => Generator.NextDouble() <= 0.5;
    }
}
