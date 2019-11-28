using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CircuitSim2.IO;

namespace CircuitSim2.Chips.IO.Inputs
{
    [Chip("Button")]
    [Serializable]
    public sealed class Button : ChipBase
    {
        [ChipProperty]
        public bool ToggleMode = false;

        [NonSerialized]
        public readonly GenericOutput<bool> Outputs;

        private Button()
        {
            InputSet = new NoInputs();
            OutputSet = (Outputs = new GenericOutput<bool>(this));
        }

        private bool state = false;

        public void Press()
        {
            if (ToggleMode)
            {
                state = !state;
            }
            else
            {
                state = true;
            }

            Tick();
        }

        public void Release()
        {
            if (!ToggleMode)
            {
                state = false;

                Tick();
            }
        }

        [NonSerialized]
        private bool _out;

        public override void Compute() => _out = state;

        public override void Commit() => Outputs.Out.Value = _out;
    }

    public abstract class Switch<T> : ChipBase
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

        public Switch()
        {
            InputSet = new NoInputs();
            OutputSet = (Outputs = new GenericOutput<T>(this));
        }

        [NonSerialized]
        private T _out;
        public sealed override void Compute() => _out = State ? OnValue : OffValue;

        public sealed override void Commit() => Outputs.Out.Value = _out;
    }
}
