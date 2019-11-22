using CircuitSim2.Chips.Functors;
using CircuitSim2.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSim2.Chips.DateTime.Conversion
{
    [Chip("DateTimeToString")]
    public class ToString : UnaryFunctor<System.DateTime, string>
    {
        public ToString(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToString(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToString(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override string Func(System.DateTime Value) => Value.ToString();
    }

    [Chip("DateTimeToLong")]
    public class ToLong : UnaryFunctor<System.DateTime, long>
    {
        public ToLong(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        public ToLong(ChipBase ParentChip) : this(ParentChip, ParentChip?.Engine)
        {
        }

        public ToLong(Engine.Engine Engine) : this(null, Engine)
        {
        }

        public override long Func(System.DateTime Value) => Value.Ticks;
    }

    [Chip("DateTimeDecompose")]
    [PureChip]
    public class Decompose : ChipBase
    {
        public readonly GenericInput<System.DateTime> Inputs;

        public class OutputType : OutputSetBase
        {
            public readonly Output<int> Day;
            public readonly Output<int> DayOfWeek;
            public readonly Output<int> DayOfYear;
            public readonly Output<int> Hour;
            public readonly Output<int> Millisecond;
            public readonly Output<int> Minute;
            public readonly Output<int> Month;
            public readonly Output<int> Second;
            public readonly Output<long> Ticks;
            public readonly Output<int> Year;

            public OutputType(Decompose Chip) : base(new OutputBase[] { 
                new Output<int>("Day", Chip, 0), 
                new Output<int>("DayOfWeek", Chip, 1),
                new Output<int>("DayOfYear", Chip, 2),
                new Output<int>("Hour", Chip, 3),
                new Output<int>("Millisecond", Chip, 4),
                new Output<int>("Minute", Chip, 5),
                new Output<int>("Month", Chip, 6),
                new Output<int>("Second", Chip, 7),
                new Output<long>("Ticks", Chip, 8),
                new Output<int>("Year", Chip, 9),
            })
            {
                Day = this["Day"] as Output<int>;
                DayOfWeek = this["DayOfWeek"] as Output<int>;
                DayOfYear = this["DayOfYear"] as Output<int>;
                Hour = this["Hour"] as Output<int>;
                Millisecond = this["Millisecond"] as Output<int>;
                Minute = this["Minute"] as Output<int>;
                Month = this["Month"] as Output<int>;
                Second = this["Second"] as Output<int>;
                Ticks = this["Ticks"] as Output<long>;
                Year = this["Year"] as Output<int>;
            }
        }

        public readonly OutputType Outputs;

        public Decompose(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = (Inputs = new GenericInput<System.DateTime>(this));
            OutputSet = (Outputs = new OutputType(this));
        }

        private System.DateTime ts;
        public override void Compute()
        {
            ts = Inputs.A.Value;   
        }

        public override void Commit()
        {
            Outputs.Day.Value = ts.Day;

            Outputs.DayOfWeek.Value = (int)(ts.DayOfWeek);
            Outputs.DayOfYear.Value = ts.DayOfYear;
            Outputs.Hour.Value = ts.Hour;
            Outputs.Millisecond.Value = ts.Millisecond;
            Outputs.Minute.Value = ts.Minute;
            Outputs.Month.Value = ts.Month;
            Outputs.Second.Value = ts.Second;
            Outputs.Ticks.Value = ts.Ticks;
            Outputs.Year.Value = ts.Year;
        }
    }
}
