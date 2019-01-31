﻿using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Single.Conversion
{
    public sealed class ToByte : UnaryFunctor<float, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base("SingleToByte", a => (byte) a, Engine)
        {

        }
    }

    public sealed class ToChar : UnaryFunctor<float, char>
    {
        public ToChar(Engine.Engine Engine = null) : base("SingleToChar", a => (char) a, Engine)
        {

        }
    }

    public sealed class ToInteger : UnaryFunctor<float, int>
    {
        public ToInteger(Engine.Engine Engine = null) : base("SingleToInteger", a => (int)a, Engine)
        {

        }
    }

    public sealed class ToLong : UnaryFunctor<float, long>
    {
        public ToLong(Engine.Engine Engine = null) : base("SingleToLong", a => (long) a, Engine)
        {

        }
    }

    public sealed class ToDouble : UnaryFunctor<float, double>
    {
        public ToDouble(Engine.Engine Engine = null) : base("SingleToDouble", a => a, Engine)
        {

        }
    }

    public sealed class ToString : UnaryFunctor<float, string>
    {
        public ToString(Engine.Engine Engine = null) : base("SingleToString", a => a.ToString(), Engine)
        {

        }
    }
}
