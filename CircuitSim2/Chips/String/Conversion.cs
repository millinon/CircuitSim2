using CircuitSim2.Chips.Functors;
using System;

namespace CircuitSim2.Chips.String.Conversion
{
    [Chip("StringToByte")]
    [Serializable]
    public sealed class ToByte : UnaryFunctor<string, byte>
    {
        public override byte Func(string Value) => byte.Parse(Value);
    }

    [Chip("StringToInteger")]
    [Serializable]
    public sealed class ToInteger : UnaryFunctor<string, int>
    {
        public override int Func(string Value) => int.Parse(Value);
    }

    [Chip("StringToSingle")]
    [Serializable]
    public sealed class ToSingle : UnaryFunctor<string, float>
    {
        public override float Func(string Value) => float.Parse(Value);
    }

    [Chip("StringToDouble")]
    [Serializable]
    public sealed class ToDouble : UnaryFunctor<string, double>
    {
        public override double Func(string Value) => double.Parse(Value);
    }

    [Chip("StringToDateTime")]
    [Serializable]
    public sealed class ToDateTime : UnaryFunctor<string, System.DateTime>
    {
        public override System.DateTime Func(string Value) => System.DateTime.Parse(Value);
    }

}
