using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

#if GRAPHICS
using System.Drawing;
#endif

using CircuitSim2.IO;

namespace CircuitSim2
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class Chip : Attribute
    {
        public readonly string Name;
        public readonly string Description;

        public Chip(string Name, string Description = null)
        {
            this.Name = Name;
            this.Description = Description;
        }
    }
    
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PureChip : Attribute
    {
        
    }

    namespace Chips
    {
        [DebuggerDisplay("{Name}")]
        public abstract class ChipBase : IDisposable
        {
            private readonly object lock_obj;

            public readonly string ID;

            private bool autotick = true;
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

            private Chip ChipAttr
            {
                get
                {
                    var attrs = GetType().GetCustomAttributes(false).Where(attr => attr.GetType() == typeof(Chip));
                    if (!attrs.Any()) throw new ArgumentException("Chip missing [Chip()] attribute");

                    return attrs.First() as Chip;
                }
            }

            public bool IsPure => GetType().GetCustomAttributes(false).Any(attr => attr.GetType() == typeof(PureChip));

            public string Name
            {
                get
                {
                    return ChipAttr.Name;
                }
            }

            public string Description
            {
                get
                {
                    return ChipAttr.Description;
                }
            }

            public readonly ChipBase ParentChip;

            protected readonly List<ChipBase> ChildChips;
            
            public ChipBase(ChipBase ParentChip, Engine.Engine Engine)
            {
                this.ParentChip = ParentChip;
                this.Engine = Engine;

                ChildChips = new List<ChipBase>();

                ID = Guid.NewGuid().ToString();

                lock_obj = new object();

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

#if GRAPHICS
            public struct SizeVec
            {
                public double Length;
                public double Width;
                public double Height;
            };

            public virtual SizeVec Size
            {
                get
                {
                    return new SizeVec
                    {
                        Length = 2.0,
                        Width = 2.0,
                        Height = 1.0,
                    };
                }
            }

            public struct PositionVec
            {
                public double X;
                public double Y;
                public double Z;
            }

            public PositionVec Position;

            public struct RotationVec
            {
                public double Pitch;
                public double Yaw;
                public double Roll;
            }

            public RotationVec Rotation;

            public double _scale = 1.0;
            public double Scale
            {
                get
                {
                    if(ParentChip != null)
                    {
                        return _scale * ParentChip.Scale;
                    }

                    return _scale;
                }
                set
                {
                    _scale = value;
                }
            }

            private bool _visible = true;
            public bool Visible
            {
                get
                {
                    if (ParentChip != null && !ParentChip.Visible)
                    {
                        return false;
                    }

                    return _visible;
                }
                set
                {
                    _visible = value;
                }
            }

            public bool ChildrenVisible = false;

            public virtual void Draw2D(Graphics G)
            {
                if (!Visible) return;


            }
#endif


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

                        foreach(var chip in ChildChips)
                        {
                            chip.Dispose();
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
