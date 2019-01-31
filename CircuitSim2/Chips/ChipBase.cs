namespace CircuitSim2.Chips
{
    public abstract class ChipBase
    {
        public bool AutoTick = true;

        public readonly Engine.Engine Engine;
        public readonly string Name;

        public ChipBase(string Name, Engine.Engine Engine = null)
        {
            this.Name = Name;
            this.Engine = Engine;
        }

        public CircuitSim2.IO.InputSetBase InputSet
        {
            get; protected set;
        }

        public CircuitSim2.IO.OutputSetBase OutputSet
        {
            get; protected set;
        }

        public virtual void Compute()
        {

        }

        public abstract void Output();

        public virtual void Tick()
        {
            Compute();

            Output();
        }

        public virtual void Detach()
        {
            InputSet.Detach();
            OutputSet.Detach();
        }
    }
}
