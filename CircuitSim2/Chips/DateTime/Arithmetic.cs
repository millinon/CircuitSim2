using CircuitSim2.Chips.Functors;
using System;

namespace CircuitSim2.Chips.DateTime.Arithmetic
{
    [Chip("DateTimeAddSpan")]
    [Serializable]
    public sealed class AddSpan : BinaryFunctor<System.DateTime, System.TimeSpan, System.DateTime>
    {
        public override System.DateTime Func(System.DateTime Val1, System.TimeSpan Val2) => Val1 + Val2;
    }

    [Chip("DateTimeSubtract")]
    [Serializable]
    public sealed class Subtract : BinaryFunctor<System.DateTime, System.DateTime, System.TimeSpan>
    {
        public override System.TimeSpan Func(System.DateTime Val1, System.DateTime Val2) => Val1 - Val2;
    }

    [Chip("DateTimeSubtractSpan")]
    [Serializable]
    public sealed class SubtractSpan : BinaryFunctor<System.DateTime, System.TimeSpan, System.DateTime>
    {
        public override System.DateTime Func(System.DateTime Val1, System.TimeSpan Val2) => Val1 - Val2;
    }

    [Chip("TimeSpanClamp")]
    [Serializable]
    public sealed class Clamp : Clamp<System.DateTime>
    {
    }
}
