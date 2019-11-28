using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using static System.Math;

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

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ChipProperty : Attribute
    {

    }

    namespace Chips
    {
        [DebuggerDisplay("{Name}")]
        [Serializable]
        public abstract class ChipBase : IDisposable
        {
            [NonSerialized]
            private readonly object lock_obj;

            public readonly string ID;

            private bool autotick = true;
            public bool AutoTick
            {
                get { lock (lock_obj) return autotick; }
                set { lock (lock_obj) autotick = value; }
            }

            [NonSerialized]
            private bool haveerror;
            public bool HaveError
            {
                get
                {
                    if (SubChips.Any(chip => chip.HaveError))
                    {
                        return true;
                    }
                    lock (lock_obj) return haveerror;
                }
                private set { lock (lock_obj) haveerror = value; }
            }

            [NonSerialized]
            private Engine.Engine engine;
            public Engine.Engine Engine
            {
                get
                {
                    lock (lock_obj) return engine;
                }
                set
                {
                    lock (lock_obj)
                    {
                        engine?.Unregister(this);

                        engine = value;

                        foreach(var subchip in SubChips)
                        {
                            subchip.Engine = value;
                        }

                        engine?.Register(this);
                    }
                }
            }

            [NonSerialized]
            private ChipBase parentchip;
            public ChipBase ParentChip
            {
                get
                {
                    lock(lock_obj) return parentchip;
                } set
                {
                    lock (lock_obj)
                    {
                        parentchip = value;

                        Engine = parentchip?.Engine;
                    }
                }
            }

            [NonSerialized]
            private readonly Dictionary<string, ChipBase> ChildChips;

            private Chip ChipAttr
            {
                get
                {
                    var attrs = GetType().GetCustomAttributes(false).Where(attr => attr.GetType() == typeof(Chip));
                    if (!attrs.Any()) throw new ArgumentException("Chip missing [Chip()] attribute");

                    return attrs.First() as Chip;
                }
            }

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

            public bool IsPure => GetType().GetCustomAttributes(false).Any(attr => attr.GetType() == typeof(PureChip));

            public IEnumerable<ChipBase> SubChips => ChildChips.Values;

            protected void AddSubChip(ChipBase Chip)
            {
                ChildChips[Chip.ID] = Chip;
            }

            protected void RemoveSubChip(ChipBase Chip)
            {
                ChildChips.Remove(ID);
            }

            public ChipBase()
            {
                ChildChips = new Dictionary<string, ChipBase>();

                ID = Guid.NewGuid().ToString();

                lock_obj = new object();
            }

            [NonSerialized]
            private InputSetBase inputset;
            public InputSetBase InputSet
            {
                get { lock (lock_obj) return inputset; }
                protected set { lock (lock_obj) inputset = value; }
            }

            [NonSerialized]
            private OutputSetBase outputset;
            public OutputSetBase OutputSet
            {
                get { lock (lock_obj) return outputset; }
                protected set { lock (lock_obj) outputset = value; }
            }

            public virtual void Compute()
            {

            }

            public virtual void Commit()
            {

            }

            public virtual void Reset()
            {
                this.HaveError = false;

                foreach (var chip in SubChips)
                {
                    chip.Reset();
                }
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

                        Commit();
                    }
                }
                catch (Exception)
                {
                    HaveError = true;
                }
            }

            public void SuperTick()
            {
                Tick();

                foreach (var chip in SubChips)
                {
                    chip.SuperTick();
                }
            }

            public virtual void Detach()
            {
                InputSet?.Detach();
                OutputSet?.Detach();
            }

            #region Graphics Support

            public virtual SizeVec size
            {
                get
                {
                    var subchips = SubChips.Count();

                    var inputs = InputSet.AllInputs.Count();
                    var outputs = OutputSet.AllOutputs.Count();

                    var maxio = Max(inputs, outputs) + 1.0;

                    return new SizeVec
                    {
                        Length = Math.Max((int)((subchips + 2) * 4), 2),
                        Width = Math.Max((int)(subchips * 1.5), maxio * 2.5),
                        Height = 1,
                    };
                }
            }

            public SizeVec Size
            {
                get
                {
                    var s = Scale;

                    return new SizeVec
                    {
                        Length = s * size.Length,
                        Width = s * size.Width,
                        Height = s * size.Height,
                    };
                }
            }

            private PositionVec position = new PositionVec
            {
                X = 0.0,
                Y = 0.0,
                Z = 0.0,
            };

            public PositionVec Position
            {
                get
                {
                    if (ParentChip != null)
                    {


                        var parent_scale = ParentChip.Scale;
                        var parent_angle = ParentChip.Rotation;
                        var parent_pos = ParentChip.Position;

                        var pos = new PositionVec
                        {
                            X = parent_scale * position.X,
                            Y = parent_scale * position.Y,
                            Z = parent_scale * position.Z,
                        };

                        var rotation_matrix = new double[3][]
                        {
                            new double[3] { Cos(parent_angle.Beta)*Cos(parent_angle.Gamma), -Cos(parent_angle.Alpha)*Sin(parent_angle.Gamma)+Sin(parent_angle.Alpha)*Sin(parent_angle.Beta)*Cos(parent_angle.Gamma), Sin(parent_angle.Alpha)*Sin(parent_angle.Gamma)+Cos(parent_angle.Alpha)*Sin(parent_angle.Beta)*Cos(parent_angle.Gamma) },
                            new double[3] { Cos(parent_angle.Beta)*Sin(parent_angle.Gamma), Cos(parent_angle.Alpha)*Cos(parent_angle.Gamma)+Sin(parent_angle.Alpha)*Sin(parent_angle.Beta)*Sin(parent_angle.Gamma), -Sin(parent_angle.Alpha)*Cos(parent_angle.Gamma)+Cos(parent_angle.Alpha)*Sin(parent_angle.Beta)*Sin(parent_angle.Gamma) },
                            new double[3] { -Sin(parent_angle.Beta), Sin(parent_angle.Alpha)*Cos(parent_angle.Beta), Cos(parent_angle.Alpha)*Cos(parent_angle.Beta) }
                        };

                        return parent_pos.Add(pos.Multiply(rotation_matrix));
                    }
                    else
                    {
                        return position;
                    }
                }
                set
                {
                    position = value;
                }
            }

            public RotationVec rotation = new RotationVec
            {
                Alpha = 0.0,
                Beta = 0.0,
                Gamma = 0.0,
            };

            public RotationVec Rotation
            {
                get
                {
                    if (ParentChip != null)
                    {
                        var parent_angle = ParentChip.Rotation;

                        return new RotationVec
                        {
                            Alpha = parent_angle.Alpha + rotation.Alpha,
                            Beta = parent_angle.Beta + rotation.Beta,
                            Gamma = parent_angle.Gamma + rotation.Gamma,
                        };
                    }
                    else
                    {
                        return rotation;
                    }
                }
                set
                {
                    rotation = value;
                }
            }

            public double scale = 1.0;
            public double Scale
            {
                get
                {
                    if (ParentChip != null)
                    {
                        return scale * ParentChip.Scale;
                    }
                    else
                    {
                        return scale;
                    }
                }
                set
                {
                    scale = value;
                }
            }

            private bool visible = true;
            public bool Visible
            {
                get
                {
                    if (ParentChip != null && !ParentChip.Visible)
                    {
                        return false;
                    }
                    else
                    {
                        return visible;
                    }
                }
                set
                {
                    visible = value;
                }
            }

            private bool childrenvisible = false;

            public bool ChildrenVisible
            {
                get
                {
                    return childrenvisible;
                }

                set
                {
                    childrenvisible = value;

                    foreach (var chip in SubChips)
                    {
                        chip.ChildrenVisible = true;
                    }
                }
            }
            #endregion

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

                        Detach();

                        foreach (var chip in SubChips)
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
