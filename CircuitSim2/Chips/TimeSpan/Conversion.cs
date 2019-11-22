using CircuitSim2.Chips.Functors;
using CircuitSim2.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSim2.Chips.TimeSpan.Conversion
{
    [Chip("TimeSpanToString")]
    [PureChip]
    public sealed class ToString : UnaryFunctor<System.TimeSpan, string>
    {
        public ToString(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToString(ChipBase ParentChip) : base(ParentChip, ParentChip?.Engine)
        {
        }

        public ToString(Engine.Engine Engine) : base(null, Engine)
        {
        }

        public override string Func(System.TimeSpan Value) => Value.ToString();
    }

    [Chip("TimeSpanDecompose")]
    [PureChip]
    public sealed class Decompose : ChipBase
    {
        public readonly CircuitSim2.IO.GenericInput<System.TimeSpan> Inputs;

        public class OutputType : CircuitSim2.IO.OutputSetBase
        {
            public readonly Output<int>     Seconds;
            public readonly Output<int>     Minutes;
            public readonly Output<int>     Milliseconds;
            public readonly Output<int>     Hours;
            public readonly Output<int>     Days;
            public readonly Output<long>    Ticks;
            public readonly Output<double>  TotalHours;
            public readonly Output<double>  TotalMilliseconds;
            public readonly Output<double>  TotalMinutes;
            public readonly Output<double>  TotalSeconds;

            public OutputType(Decompose Chip) : base(new OutputBase[]
            {
                new Output<int>("Seconds", Chip, 0),
                new Output<int>("Minutes", Chip, 1),
                new Output<int>("Milliseconds", Chip, 2),
                new Output<int>("Hours", Chip, 3),
                new Output<int>("Days", Chip, 4),
                new Output<long>("Ticks", Chip, 5),
                new Output<double>("TotalHours", Chip, 6),
                new Output<double>("TotalMilliseconds", Chip, 7),
                new Output<double>("TotalMinutes", Chip, 8),
                new Output<double>("TotalSeconds", Chip, 9),
            })
            {
                Seconds = this["Seconds"] as Output<int>;
                Minutes = this["Minutes"] as Output<int>;
                Milliseconds = this["Milliseconds"] as Output<int>;
                Hours = this["Hours"] as Output<int>;
                Days = this["Days"] as Output<int>;
                Ticks = this["Ticks"] as Output<long>;
                TotalHours = this["TotalHours"] as Output<double>;
                TotalMilliseconds = this["TotalMilliseconds"] as Output<double>;
                TotalMinutes = this["TotalMinutes"] as Output<double>;
                TotalSeconds = this["TotalSeconds"] as Output<double>;
            }
        }

        public readonly OutputType Outputs;

        public Decompose(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = (Inputs = new GenericInput<System.TimeSpan>(this));
            OutputSet = (Outputs = new OutputType(this));
        }

        private System.TimeSpan ts;
        public override void Compute()
        {
            ts = Inputs.A.Value;
        }

        public override void Commit()
        {
            Outputs.Seconds.Value = ts.Seconds;
            Outputs.Minutes.Value = ts.Minutes;
            Outputs.Milliseconds.Value = ts.Milliseconds;
            Outputs.Hours.Value = ts.Hours;
            Outputs.Days.Value = ts.Days;
            Outputs.Ticks.Value = ts.Ticks;
            Outputs.TotalHours.Value = ts.TotalHours;
            Outputs.TotalMilliseconds.Value = ts.TotalMilliseconds;
            Outputs.TotalMinutes.Value = ts.TotalMinutes;
            Outputs.TotalSeconds.Value = ts.TotalSeconds;
        }
    }
}
