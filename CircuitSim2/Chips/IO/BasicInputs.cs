using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CircuitSim2.IO;

namespace CircuitSim2.Chips.IO.BasicInputs
{
    public sealed class GenericInput<T> : ChipBase where T : IEquatable<T>
    {
        public readonly GenericOutput<T> Outputs;

        public GenericInput(Engine.Engine Engine = null) : base("GenericInput", Engine)
        {
            Outputs = new GenericOutput<T>(this);

            InputSet = new NoInputs();
            OutputSet = Outputs;

            Value = default(T);
        }

        private T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;

                Tick();
            }
        }

        public override void Output() => Outputs.Out.Value = Value;
    }

    public sealed class Constant<T> : Chips.ChipBase where T : IEquatable<T>
    {
        public readonly GenericOutput<T> Outputs;

        public Constant(T Value, Engine.Engine Engine = null) : base("Constant", Engine)
        {
            Outputs = new GenericOutput<T>(this);

            InputSet = new NoInputs();
            OutputSet = Outputs;

            this.Value = Value;
        }

        public readonly T Value;

        public override void Output() => Outputs.Out.Value = Value;
    }

    public sealed class Button : ChipBase
    {
        public readonly GenericOutput<bool> Outputs;

        public Button(Engine.Engine Engine = null) : base("Button", Engine)
        {
            Outputs = new GenericOutput<bool>(this);

            InputSet = new NoInputs();
            OutputSet = Outputs;
        }

        private bool _state;
        public bool State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;

                Tick();
            }
        }

        public void Toggle() => State = !State;

        public override void Output() => Outputs.Out.Value = State;
    }
}
