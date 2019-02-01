using System;
using System.Linq;
using System.Diagnostics;

namespace CircuitSim2
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class Chip : Attribute
    {
        private string name;
        public virtual string Name
        {
            get { return name; }
        }

        public virtual bool IsPure
        {
            get
            {
                return false;
            }
        }

        public Chip(string Name)
        {
            this.name = Name;
        }
    }

    public class PureChip : Chip
    {
        public override bool IsPure
        {
            get
            {
                return true;
            }
        }

        public PureChip(string Name) : base(Name)
        {

        }

    }

    namespace Chips
    {
        [DebuggerDisplay("{Name}")]
        public abstract class ChipBase
        {
            public bool AutoTick = true;

            public bool HaveError = false;

            public readonly Engine.Engine Engine;

            public string Name
            {
                get
                {
                    var attrs = GetType().GetCustomAttributes(false).Where(attr => attr.GetType() == typeof(Chip) || attr.GetType() == typeof(PureChip));

                    if (!attrs.Any()) throw new ArgumentException("Chip missing [Chip()] attribute");

                    return (attrs.First() as Chip).Name;
                }
            }

            public ChipBase(Engine.Engine Engine = null)
            {
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
                try
                {
                    HaveError = false;

                    Compute();

                    Output();
                }
                catch (Exception)
                {
                    HaveError = true;
                }
            }

            public virtual void Detach()
            {
                InputSet.Detach();
                OutputSet.Detach();
            }
        }
    }
}
