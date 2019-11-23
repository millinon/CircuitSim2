using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using static System.Math;

namespace CircuitSim2.IO
{
    public enum Type
    {
        DIGITAL,
        BYTE,
        CHAR,
        INT,
        LONG,
        SINGLE,
        DOUBLE,
        STRING,
        DATETIME,
        TIMESPAN,
        CONSOLECOLOR,
    }

    public static class Type_Map
    {
        private static readonly Dictionary<System.Type, IO.Type> Dictionary;

        static Type_Map()
        {
            Dictionary = new Dictionary<System.Type, IO.Type>
            {
                [typeof(bool)] = IO.Type.DIGITAL,
                [typeof(byte)] = IO.Type.BYTE,
                [typeof(char)] = IO.Type.CHAR,
                [typeof(int)] = IO.Type.INT,
                [typeof(long)] = IO.Type.LONG,
                [typeof(float)] = IO.Type.SINGLE,
                [typeof(double)] = IO.Type.DOUBLE,
                [typeof(string)] = IO.Type.STRING,
                [typeof(DateTime)] = IO.Type.DATETIME,
                [typeof(TimeSpan)] = IO.Type.TIMESPAN,
                [typeof(ConsoleColor)] = IO.Type.CONSOLECOLOR,
            };
        }

        public static IO.Type Lookup(System.Type Type)
        {
            if (Dictionary.ContainsKey(Type)) return Dictionary[Type];

            throw new ArgumentException($"Type map lookup failed for {Type.Namespace}.{Type.Name}");
        }
    }

    public abstract class IOBase
    {
        public readonly Type Type;
        public readonly string Name;
        public readonly Chips.ChipBase Chip;
        public readonly int Index;

        protected readonly object lock_obj;

        protected IOBase(Chips.ChipBase Chip, string Name, Type Type, int Index)
        {
            this.Chip = Chip;
            this.Name = Name;
            this.Type = Type;
            this.Index = Index;

            lock_obj = new object();
        }

        public PositionVec Position
        {
            get
            {
                var is_input = this is InputBase;

                var parent_angle = Chip.Rotation;
                var parent_pos = Chip.Position;
                var parent_size = Chip.Size;

                var x_pos = (is_input ? -parent_size.Length / 2 : parent_size.Length / 2);

                var y_space = parent_size.Width - 1;
                var num_ios = is_input ? Chip.InputSet.AllInputs.Count() : Chip.OutputSet.AllOutputs.Count();
                var y_space_per_io = y_space / num_ios;
                double y_pos;

                //(size.Width/8)*-(8-i)+size.Width/2,

                y_pos = y_space_per_io * -(num_ios - Index - 0.5) + y_space / 2;

/*                if (num_ios % 2 == 1)
                {
  */                  


/*                    if(num_ios == 1)
                    {
                        y_pos = 0;
                    } else if(Index < (num_ios) / 2)
                    {
                        y_pos = y_space_per_io * (num_ios-Index);
                    } else
                    {
                        y_pos = y_space_per_io * (Index-num_ios/2);
                    } */
 /*               } else
                {
                    y_pos = y_space_per_io * (-)

                    if(Index <  num_ios / 2)
                    {
                        y_pos = -y_space_per_io * (Index + 0.5);
                    } else
                    {
                        y_pos = y_space_per_io * (Index - num_ios/2 + 0.5);
                    }
                }*/
                               
                var position = new PositionVec
                    {
                        X = x_pos,
                        Y = y_pos,
                        Z = parent_pos.Z,
                    };

                var rotation_matrix = new double[3][]
                {
                            new double[3] { Cos(parent_angle.Beta)*Cos(parent_angle.Gamma), -Cos(parent_angle.Alpha)*Sin(parent_angle.Gamma)+Sin(parent_angle.Alpha)*Sin(parent_angle.Beta)*Cos(parent_angle.Gamma), Sin(parent_angle.Alpha)*Sin(parent_angle.Gamma)+Cos(parent_angle.Alpha)*Sin(parent_angle.Beta)*Cos(parent_angle.Gamma) },
                            new double[3] { Cos(parent_angle.Beta)*Sin(parent_angle.Gamma), Cos(parent_angle.Alpha)*Cos(parent_angle.Gamma)+Sin(parent_angle.Alpha)*Sin(parent_angle.Beta)*Sin(parent_angle.Gamma), -Sin(parent_angle.Alpha)*Cos(parent_angle.Gamma)+Cos(parent_angle.Alpha)*Sin(parent_angle.Beta)*Sin(parent_angle.Gamma) },
                            new double[3] { -Sin(parent_angle.Beta), Sin(parent_angle.Alpha)*Cos(parent_angle.Beta), Cos(parent_angle.Alpha)*Cos(parent_angle.Beta) }
                };

                return parent_pos.Add(position.Multiply(rotation_matrix));
            }
        }
    }

    public abstract class InputBase : IOBase
    {
        protected OutputBase sourcebase;
        public OutputBase SourceBase
        {
            get { lock (lock_obj) { return sourcebase; } }
            protected set { sourcebase = value; }
        }

        public bool IsAttached
        {
            get { lock (lock_obj) { return sourcebase != null; } }
        }

        protected InputBase(Chips.ChipBase Chip, string Name, Type Type, int Index) : base(Chip, Name, Type, Index)
        {
            SubscribedInputs = new HashSet<InputBase>();
        }

        protected InputBase binding;
        public InputBase Binding
        {
            get { lock (lock_obj) { return binding; } }
        }
        public bool IsBound
        {
            get { lock (lock_obj) { return binding != null; } }
        }

        public abstract void Detach();

        public abstract void Attach(OutputBase Output);

        public void Bind(InputBase Input)
        {
            if (Input.Type != Type) throw new InvalidOperationException();

            Detach();

            lock (lock_obj)
            {
                Input.Subscribe(this);

                binding = Input;
            }
        }

        public void Unbind()
        {
            lock (lock_obj)
            {
                if (binding != null)
                {
                    binding.Unsubscribe(this);
                }

                binding = null;
            }
        }

        protected readonly HashSet<InputBase> SubscribedInputs;

        public IEnumerable<InputBase> Hooks
        {
            get { lock (lock_obj) { return SubscribedInputs; } }
        }

        protected void Subscribe(InputBase Input)
        {
            if (Input.Type != Type) throw new InvalidOperationException();
            lock (lock_obj)
            {
                SubscribedInputs.Add(Input);
            }
        }

        protected void Unsubscribe(InputBase Input)
        {
            lock (lock_obj)
            {
                if (SubscribedInputs.Contains(Input)) SubscribedInputs.Remove(Input);
            }
        }

        public abstract void Notify();
    }

    [DebuggerDisplay("{Chip.Name}.Inputs.{Name}: {Value}")]
    public class Input<T> : InputBase where T : IEquatable<T>
    {
        private static readonly Type sType = Type_Map.Lookup(typeof(T));

        public Input(string Name, Chips.ChipBase Chip, int Index) : base(Chip, Name, sType, Index)
        {

        }

        private Output<T> source;
        public Output<T> Source
        {
            get { lock (lock_obj) { return source; } }
            private set { lock (lock_obj) { source = value; } }
        }

        public override void Attach(OutputBase Output)
        {
            if (Output.Type != Type) throw new InvalidOperationException();

            lock (lock_obj)
            {
                source?.Detach(this);

                sourcebase = Output;
                source = Output as Output<T>;

                Output.Attach(this);
            }

            Chip.Reset();

            Chip.Engine?.RegenerateGraph();
        }

        public sealed override void Detach()
        {
            lock (lock_obj)
            {
                source?.Detach(this);

                sourcebase = null;
                source = null;
            }

            Chip.Reset();

            Chip.Engine?.RegenerateGraph();
        }

        public T Value
        {
            get
            {
                lock (lock_obj)
                {
                    if (binding != null) return (binding as Input<T>).Value;

                    return (source ?? throw new InvalidOperationException()).Value;
                }
            }
        }

        public override void Notify()
        {
            lock (lock_obj)
            {
                foreach (InputBase input in SubscribedInputs)
                {
                    input.Notify();
                }
                if (Chip.AutoTick)
                {
                    if (Chip.Engine != null) Chip.Engine.ScheduleUpdate(Chip);
                    else Chip.Tick();
                }
            }
        }
    }

    public sealed class ClockInput : Input<bool>
    {
        public ClockInput(Chips.ChipBase Chip, int Index) : base("Clk", Chip, Index)
        {
        }

        private bool last = false;

        public override void Notify()
        {
            if(Value && !last) // detect rising edge
            {
                base.Notify();
            }

            last = Value;
        }
    }

    public sealed class ClockInputOnly : InputSetBase
    {
        public readonly ClockInput Clk;

        public ClockInputOnly(Chips.ChipBase Chip) : base(new InputBase[] { new ClockInput(Chip, 0) })
        {
            Clk = this["Clk"] as ClockInput;
        }
    }

    public abstract class OutputBase : IOBase
    {
        protected OutputBase(Chips.ChipBase Chip, string Name, Type Type, int Index) : base(Chip, Name, Type, Index)
        {
            SubscribedOutputs = new HashSet<OutputBase>();
        }

        public abstract IEnumerable<InputBase> Sinks();

        public abstract void Detach();
        public abstract void Attach(InputBase Input);

        protected OutputBase binding;
        public OutputBase Binding
        {
            get { lock (lock_obj) { return binding; } }
        }

        public bool IsBound
        {
            get { lock (lock_obj) { return binding != null; } }
        }

        public void Bind(OutputBase Output)
        {
            if (Output.Type != Type) throw new InvalidOperationException();

            Detach();

            lock (lock_obj)
            {
                Output.Subscribe(this);

                binding = Output;
            }
        }

        public void Unbind()
        {
            lock (lock_obj)
            {
                if (Binding != null)
                {
                    Binding.Unsubscribe(this);
                }

                binding = null;
            }
        }

        protected void Subscribe(OutputBase Output)
        {
            if (Output.Type != Type) throw new InvalidOperationException();
            lock (lock_obj)
            {
                SubscribedOutputs.Add(Output);
            }
        }

        protected void Unsubscribe(OutputBase Output)
        {
            lock (lock_obj)
            {
                if (SubscribedOutputs.Contains(Output)) SubscribedOutputs.Remove(Output);
            }
        }

        protected readonly HashSet<OutputBase> SubscribedOutputs;
    }

    [DebuggerDisplay("{Chip.Name}.Outputs.{Name}: {Value}")]
    public sealed class Output<T> : OutputBase where T : IEquatable<T>
    {
        private static readonly Type sType = Type_Map.Lookup(typeof(T));

        public Output(string Name, Chips.ChipBase Chip, int Index) : base(Chip, Name, sType, Index)
        {

        }

        private bool havevalue;
        public bool HaveValue
        {
            get { lock (lock_obj) { return havevalue; } }
        }

        public class ValueChangedEventArgs : EventArgs
        {
            public T NewValue;
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        private T value;
        public T Value
        {
            get
            {
                lock (lock_obj)
                {
                    if (Chip.HaveError/* || !havevalue*/) throw new InvalidOperationException();

                    return value;
                }
            }
            set
            {
                lock (lock_obj)
                {
                    bool changed = false;
                    if (!Chip.IsPure)
                    {
                        changed = true;
                    }
                    else if (HaveValue)
                    {
                        changed = !Value.Equals(value);
                    }
                    else changed = true;

                    this.value = value;
                    havevalue = true;

                    if (changed)
                    {
                        ValueChanged?.Invoke(this, new ValueChangedEventArgs { NewValue = value });

                        foreach (var output in SubscribedOutputs)
                        {
                            (output as Output<T>).Value = value;
                        }
                    }

                    foreach (var sink in Sinks())
                    {
                        (sink as Input<T>).Notify();
                    }
                }
            }
        }

        private readonly HashSet<Input<T>> SinkList = new HashSet<Input<T>>();

        public override void Attach(InputBase Input)
        {
            if (Input.Type != Type) throw new InvalidOperationException();

            lock (lock_obj)
            {
                if (!SinkList.Contains(Input as Input<T>)) SinkList.Add(Input as Input<T>);
            }
        }

        public void Detach(Input<T> Input)
        {
            lock (lock_obj)
            {
                if (SinkList.Contains(Input)) SinkList.Remove(Input);
            }
        }

        public sealed override IEnumerable<InputBase> Sinks()
        {
            lock (lock_obj)
            {
                return SinkList;
            }
        }

        public sealed override void Detach()
        {
            lock (lock_obj)
            {
                foreach (var sink in Sinks())
                {
                    sink.Detach();
                }
            }
        }
    }

    public class InputSetBase
    {
        protected readonly object lock_obj;

        private readonly Dictionary<string, InputBase> InputsByName;

        public IEnumerable<InputBase> AllInputs => InputsByName.Values;

        public InputSetBase(IEnumerable<InputBase> Inputs)
        {
            lock_obj = new object();

            var dict = new Dictionary<string, InputBase>();

            foreach (var input in Inputs)
            {
                dict[input.Name] = input;
            }

            InputsByName = dict;
        }

        public InputBase this[string Name]
        {
            get
            {
                lock (lock_obj)
                {
                    if (!InputsByName.ContainsKey(Name)) throw new ArgumentException();

                    return InputsByName[Name];
                }
            }
        }

        public virtual void Detach()
        {
            lock (lock_obj)
            {
                foreach (var input in AllInputs)
                {
                    input.Detach();
                }
            }
        }
    }

    public sealed class GenericInput<T> : InputSetBase where T : IEquatable<T>
    {
        public Input<T> A;

        public GenericInput(Chips.ChipBase Chip) : base(new InputBase[] { new Input<T>("A", Chip, 0), }) => A = this["A"] as Input<T>;
    }

    public sealed class GenericInput<T, U> : InputSetBase where T : IEquatable<T> where U : IEquatable<U>
    {
        public readonly Input<T> A;
        public readonly Input<U> B;

        public GenericInput(Chips.ChipBase Chip) : base(new InputBase[] { new Input<T>("A", Chip, 0), new Input<U>("B", Chip, 1), })
        {
            A = this["A"] as Input<T>;
            B = this["B"] as Input<U>;
        }
    }

    public sealed class InputArray<T> : InputSetBase where T : IEquatable<T>
    {
        private readonly Input<T>[] Array;

        public InputArray(Chips.ChipBase Chip, int Size) : base(Enumerable.Range(0, Size).Select(idx => new Input<T>($"{idx}", Chip, idx)))
        {
            Array = new Input<T>[Size];
            for (int idx = 0; idx < Size; idx++)
            {
                Array[idx] = this[$"{idx}"] as Input<T>;
            }
        }

        public Input<T> this[int idx]
        {
            get
            {
                lock (lock_obj) { return Array[idx]; }
            }
        }

        public int Length => Array.Length;
    }

    public sealed class NoInputs : InputSetBase
    {
        public NoInputs() : base(new InputBase[] { })
        {

        }
    }

    public class OutputSetBase
    {
        protected readonly object lock_obj;

        protected readonly Dictionary<string, OutputBase> OutputsByName;

        public IEnumerable<OutputBase> AllOutputs => OutputsByName.Values;

        public OutputSetBase(IEnumerable<OutputBase> Outputs)
        {
            lock_obj = new object();

            var dict = new Dictionary<string, OutputBase>();

            foreach (var output in Outputs)
            {
                dict[output.Name] = output;
            }

            OutputsByName = dict;
        }

        public OutputBase this[string Name]
        {
            get
            {
                lock (lock_obj)
                {
                    if (!OutputsByName.ContainsKey(Name)) throw new ArgumentException();

                    return OutputsByName[Name];
                }
            }
        }

        public virtual void Detach()
        {
            lock (lock_obj)
            {
                foreach (var output in AllOutputs)
                {
                    output.Detach();
                }
            }
        }
    }

    public sealed class GenericOutput<T> : OutputSetBase where T : IEquatable<T>
    {
        public readonly Output<T> Out;

        public GenericOutput(Chips.ChipBase Chip) : base(new OutputBase[] { new Output<T>("Out", Chip, 0), }) => Out = this["Out"] as Output<T>;
    }

    public sealed class OutputArray<T> : OutputSetBase where T : IEquatable<T>
    {
        private readonly Output<T>[] Array;

        public OutputArray(Chips.ChipBase Chip, int Size) : base(Enumerable.Range(0, Size).Select(idx => new Output<T>($"{idx}", Chip, idx)))
        {
            Array = new Output<T>[Size];
            for (int idx = 0; idx < Size; idx++)
            {
                Array[idx] = this[$"{idx}"] as Output<T>;
            }
        }

        public Output<T> this[int idx]
        {
            get
            {
                lock (lock_obj) { return Array[idx]; }
            }
        }

        public int Length => Array.Length;
    }

    public sealed class NoOutputs : OutputSetBase
    {
        public NoOutputs() : base(new OutputBase[] { })
        {

        }
    }
}