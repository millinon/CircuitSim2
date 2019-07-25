using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CircuitSim2.IO;

namespace CircuitSim2.Chips.IO.BasicInputs
{
    [Chip("GenericInput")]
    public sealed class GenericInput<T> : ChipBase where T : IEquatable<T>
    {
        public readonly GenericOutput<T> Outputs;

        private GenericInput(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            Outputs = new GenericOutput<T>(this);

            InputSet = new NoInputs();
            OutputSet = Outputs;

            Value = default(T);
        }

        public GenericInput(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public GenericInput(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public GenericInput() : this(null, null)
        {
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

    [Chip("Constant")]
    [PureChip]
    public sealed class Constant<T> : Chips.ChipBase where T : IEquatable<T>
    {
        public readonly GenericOutput<T> Outputs;

        public Constant(T Value, ChipBase ParentChip) : this(Value, ParentChip, ParentChip?.Engine)
        {
        }

        public Constant(T Value, Engine.Engine Engine) : this(Value, null, Engine)
        {
        }

        public Constant(T Value) : this(Value, null, null)
        {
        }

        private Constant(T Value, ChipBase ParentChip, Engine.Engine Engine ) : base(ParentChip, Engine)
        {
            Outputs = new GenericOutput<T>(this);

            InputSet = new NoInputs();
            OutputSet = Outputs;

            this.Value = Value;

            Tick();
        }

        public readonly T Value;

        public override void Output() => Outputs.Out.Value = Value;
    }

    [Chip("Button")]
    public sealed class Button : ChipBase
    {
        public readonly GenericOutput<bool> Outputs;

        public Button(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Button(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public Button() : this(null, null)
        {
        }

        private Button(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
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
