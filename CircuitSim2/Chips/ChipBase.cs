using System;
using System.Linq;
using System.Diagnostics;

using CircuitSim2.IO;

namespace CircuitSim2
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class Chip : Attribute
    {
        private string name;
        public virtual string Name => name;

        public virtual bool IsPure => false;

        public Chip(string Name)
        {
            this.name = Name;
        }
    }

    public class PureChip : Chip
    {
        public override bool IsPure => true;

        public PureChip(string Name) : base(Name)
        {

        }
    }

    namespace Chips
    {
        [DebuggerDisplay("{Name}")]
        public abstract class ChipBase : IDisposable
        {
            private readonly object lock_obj;

            public readonly string ID;

            private bool autotick;
            public bool AutoTick
            {
                get { lock (lock_obj) return autotick; }
                set { lock (lock_obj) autotick = value; }
            }

            private bool haveerror;
            public bool HaveError
            {
                get { lock (lock_obj) return haveerror; }
                private set { lock (lock_obj) haveerror = value; }
            }

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

            public bool IsPure
            {
                get
                {
                    var attrs = GetType().GetCustomAttributes(false).Where(attr => attr.GetType() == typeof(Chip) || attr.GetType() == typeof(PureChip));

                    if (!attrs.Any()) throw new ArgumentException("Chip missing [Chip()] attribute");

                    return (attrs.First() as Chip).IsPure;
                }
            }

            public ChipBase(Engine.Engine Engine)
            {
                this.Engine = Engine;

                ID = Guid.NewGuid().ToString();

                lock_obj = new object();

                AutoTick = (Engine == null);

                Engine?.Register(this);
            }

            private InputSetBase inputset;
            public InputSetBase InputSet
            {
                get { lock (lock_obj) return inputset; }
                protected set { lock (lock_obj) inputset = value; }
            }

            private OutputSetBase outputset;
            public OutputSetBase OutputSet
            {
                get { lock (lock_obj) return outputset; }
                protected set { lock (lock_obj) outputset = value; }
            }

            public virtual void Compute()
            {

            }

            public virtual void Output()
            {

            }

#if DEBUG
            public ulong TickCount
            {
                get; private set;
            } = 0;
#endif

            public virtual void Tick()
            {
#if DEBUG
                TickCount++;
#endif
                try
                {
                    HaveError = false;

                    lock (lock_obj)
                    {
                        Compute();

                        Output();
                    }
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

            #region IDisposable Support
            private bool disposedValue = false;

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        lock (lock_obj)
                        {
                            Engine?.Unregister(this);
                        }
                    }

                    disposedValue = true;
                }
            }
            public void Dispose()
            {
                Dispose(true);
            }
            #endregion
        }
    }
}
