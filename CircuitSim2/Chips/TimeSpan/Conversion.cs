using CircuitSim2.Chips.Functors;
using CircuitSim2.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSim2.Chips.TimeSpan.Conversion
{
    [Chip("TimeSpanFromMilliseconds")]
    [Serializable]
    [PureChip]
    public sealed class FromMilliseconds : UnaryFunctor<int, System.TimeSpan>
    {
        public override System.TimeSpan Func(int Value) => System.TimeSpan.FromMilliseconds(Value);
    }

    [Chip("TimeSpanFromSeconds")]
    [Serializable]
    [PureChip]
    public sealed class FromSeconds : UnaryFunctor<int, System.TimeSpan>
    {
        public override System.TimeSpan Func(int Value) => System.TimeSpan.FromSeconds(Value);
    }

    [Chip("TimeSpanFromMinutes")]
    [Serializable]
    [PureChip]
    public sealed class FromMinutes : UnaryFunctor<int, System.TimeSpan>
    {
        public override System.TimeSpan Func(int Value) => System.TimeSpan.FromMinutes(Value);
    }

    [Chip("TimeSpanFromHours")]
    [Serializable]
    [PureChip]
    public sealed class FromHours : UnaryFunctor<int, System.TimeSpan>
    {
        public override System.TimeSpan Func(int Value) => System.TimeSpan.FromHours(Value);
    }

    [Chip("TimeSpanFromDays")]
    [Serializable]
    [PureChip]
    public sealed class FromDays : UnaryFunctor<int, System.TimeSpan>
    {
        public override System.TimeSpan Func(int Value) => System.TimeSpan.FromDays(Value);
    }

    [Chip("TimeSpanFromTicks")]
    [Serializable]
    [PureChip]
    public sealed class FromTicks : UnaryFunctor<long, System.TimeSpan>
    {
        public override System.TimeSpan Func(long Value) => System.TimeSpan.FromTicks(Value);
    }

    [Chip("TimeSpanToString")]
    [Serializable]
    [PureChip]
    public sealed class ToString : UnaryFunctor<System.TimeSpan, string>
    {
        public override string Func(System.TimeSpan Value) => Value.ToString();
    }

    [Chip("TimeSpanDecompose")]
    [Serializable]
    [PureChip]
    public sealed class Decompose : ChipBase
    {
        [NonSerialized]
        public readonly CircuitSim2.IO.GenericInput<System.TimeSpan> Inputs;

        public class OutputType : CircuitSim2.IO.OutputSetBase
        {
            public readonly Output<int> Seconds;
            public readonly Output<int> Minutes;
            public readonly Output<int> Milliseconds;
            public readonly Output<int> Hours;
            public readonly Output<int> Days;
            public readonly Output<long> Ticks;
            public readonly Output<double> TotalHours;
            public readonly Output<double> TotalMilliseconds;
            public readonly Output<double> TotalMinutes;
            public readonly Output<double> TotalSeconds;

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

        [NonSerialized]
        public readonly OutputType Outputs;

        public Decompose()
        {
            InputSet = (Inputs = new GenericInput<System.TimeSpan>(this));
            OutputSet = (Outputs = new OutputType(this));
        }

        [NonSerialized]
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
