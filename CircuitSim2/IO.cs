using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
    }

    public static class Type_Map
    {
        private static readonly IReadOnlyDictionary<System.Type, IO.Type> Dictionary;

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
                [typeof(string)] = IO.Type.STRING
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

        protected readonly object lock_obj;

        protected IOBase(Chips.ChipBase Chip, string Name, Type Type)
        {
            this.Chip = Chip;
            this.Name = Name;
            this.Type = Type;

            lock_obj = new object();
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
            get { lock(lock_obj) { return sourcebase != null; } }
        }

        protected InputBase(Chips.ChipBase Chip, string Name, Type Type) : base(Chip, Name, Type)
        {
            SubscribedInputs = new HashSet<InputBase>();
        }

        protected InputBase Binding;

        public bool IsBound
        {
            get { lock (lock_obj) { return Binding != null; } }
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

                Binding = Input;
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

                Binding = null;
            }
        }

        protected readonly HashSet<InputBase> SubscribedInputs;

        public IEnumerable<InputBase> Hooks {
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
    public sealed class Input<T> : InputBase where T : IEquatable<T>
    {
        private static readonly Type sType = Type_Map.Lookup(typeof(T));

        public Input(string Name, Chips.ChipBase Chip) : base(Chip, Name, sType)
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

            Chip.Engine?.RegenerateGraph();
        }

        public T Value
        {
            get
            {
                lock (lock_obj)
                {
                    if (Binding != null) return (Binding as Input<T>).Value;

                    return (source ?? throw new InvalidOperationException()).Value;
                }
            }
        }

        public sealed override void Notify()
        {
            lock (lock_obj)
            {
                foreach (InputBase input in SubscribedInputs)
                {
                    input.Notify();
                }

                if (Chip.AutoTick)
                {
                    Chip.Tick();
                }
                else
                {
                    Chip.Engine?.ScheduleUpdate(Chip);
                }
            }
        }
    }

    public abstract class OutputBase : IOBase
    {
        protected OutputBase(Chips.ChipBase Chip, string Name, Type Type) : base(Chip, Name, Type)
        {

        }

        public abstract IEnumerable<InputBase> Sinks();

        public abstract void Detach();
        public abstract void Attach(InputBase Input);
    }

    [DebuggerDisplay("{Chip.Name}.Outputs.{Name}: {Value}")]
    public sealed class Output<T> : OutputBase where T : IEquatable<T>
    {
        private static readonly Type sType = Type_Map.Lookup(typeof(T));

        public Output(string Name, Chips.ChipBase Chip) : base(Chip, Name, sType)
        {

        }

        private bool havevalue;
        public bool HaveValue
        {
            get { lock (lock_obj) { return havevalue; } }
        }

        public class ValueChangedEventArgs
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
                    if (Chip.HaveError || !havevalue) throw new InvalidOperationException();

                    return value;
                }
            }
            set
            {
                lock (lock_obj)
                {
                    bool changed = false;
                    if (! Chip.IsPure)
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

        private readonly IReadOnlyDictionary<string, InputBase> InputsByName;

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

        public GenericInput(Chips.ChipBase Chip) : base(new InputBase[] { new Input<T>("A", Chip), }) => A = this["A"] as Input<T>;
    }

    public sealed class GenericInput<T, U> : InputSetBase where T : IEquatable<T> where U : IEquatable<U>
    {
        public readonly Input<T> A;
        public readonly Input<U> B;

        public GenericInput(Chips.ChipBase Chip) : base(new InputBase[] { new Input<T>("A", Chip), new Input<U>("B", Chip), })
        {
            A = this["A"] as Input<T>;
            B = this["B"] as Input<U>;
        }
    }

    public sealed class InputArray<T> : InputSetBase where T : IEquatable<T>
    {
        private readonly Input<T>[] Array;

        public InputArray(Chips.ChipBase Chip, int Size) : base(Enumerable.Range(0, Size).Select(idx => new Input<T>($"{idx}", Chip)))
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

        protected readonly IReadOnlyDictionary<string, OutputBase> OutputsByName;

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

        public GenericOutput(Chips.ChipBase Chip) : base(new OutputBase[] { new Output<T>("Out", Chip), }) => Out = this["Out"] as Output<T>;
    }

    public sealed class NoOutputs : OutputSetBase
    {
        public NoOutputs() : base(new OutputBase[] { })
        {

        }
    }
}