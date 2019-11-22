using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CircuitSim2.IO;

namespace CircuitSim2.Chips.IO.Inputs
{
    [Chip("Button")]
    public sealed class Button : ChipBase
    {
        [ChipProperty]
        public bool ToggleMode = false;

        public readonly GenericOutput<bool> Outputs;

        public Button(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public Button(Engine.Engine Engine) : this(null, Engine)
        {
        }

        private Button(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = new NoInputs();
            OutputSet = (Outputs = new GenericOutput<bool>(this));
        }

        private bool _state = false;

        public void Press()
        {
            if (ToggleMode)
            {
                _state = !_state;
            } else
            {
                _state = true;
            }
        }

        public void Release()
        {
            if (!ToggleMode)
            {
                _state = false;
            }
        }

        private bool _out;

        public override void Compute() => _out = _state;

        public override void Commit() => Outputs.Out.Value = _out;
    }

    public abstract class Switch<T> : ChipBase where T:IEquatable<T>
    {
        private bool state = false;
        public bool State
        {
            get => state;
            set
            {
                state = value;

                Tick();
            }
        }

        public void Toggle() => State = !State;

        private T offValue = default;
        [ChipProperty]
        public T OffValue
        {
            get => offValue;
            set
            {
                offValue = value;

                Tick();
            }
        }

        private T onValue = default;
        [ChipProperty]
        public T OnValue
        {
            get => onValue;
            set
            {
                onValue = value;

                Tick();
            }
        }

        public readonly GenericOutput<T> Outputs;

        public Switch(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = new NoInputs();
            OutputSet = (Outputs = new GenericOutput<T>(this));
        }

        private T _out;
        public sealed override void Compute() => _out = State ? OnValue : OffValue;

        public sealed override void Commit() => Outputs.Out.Value = _out;
    }
}
