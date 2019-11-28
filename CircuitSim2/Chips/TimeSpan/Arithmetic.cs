using CircuitSim2.Chips.Functors;
using System;

namespace CircuitSim2.Chips.TimeSpan.Arithmetic
{
    [Chip("TimeSpanAdd")]
    [Serializable]
    public sealed class Add : BinaryFunctor<System.TimeSpan, System.TimeSpan, System.TimeSpan>
    {
        public override System.TimeSpan Func(System.TimeSpan Val1, System.TimeSpan Val2) => Val1 + Val2;
    }

    [Chip("TimeSpanSubtract")]
    [Serializable]
    public sealed class Subtract : BinaryFunctor<System.TimeSpan, System.TimeSpan, System.TimeSpan>
    {
        public override System.TimeSpan Func(System.TimeSpan Val1, System.TimeSpan Val2) => Val1 + Val2;
    }

    [Chip("TimeSpanClamp")]
    [Serializable]
    public sealed class Clamp : Clamp<System.TimeSpan>
    {
    }
}
