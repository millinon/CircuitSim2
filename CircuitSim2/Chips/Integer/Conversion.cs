﻿using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Integer.Conversion
{
    public sealed class ToByte : UnaryFunctor<int, byte>
    {
        public ToByte(Engine.Engine Engine = null) : base("IntegerToByte", a => (byte) a, Engine)
        {

        }
    }

    public sealed class ToChar : UnaryFunctor<int, char>
    {
        public ToChar(Engine.Engine Engine = null) : base("IntegerToChar", a => (char) a, Engine)
        {

        }
    }

    public sealed class ToLong : UnaryFunctor<int, long>
    {
        public ToLong(Engine.Engine Engine = null) : base("IntegerToLong", a => a, Engine)
        {

        }
    }

    public sealed class ToSingle : UnaryFunctor<int, float>
    {
        public ToSingle(Engine.Engine Engine = null) : base("IntegerToSingle", a => a, Engine)
        {

        }
    }

    public sealed class ToDouble : UnaryFunctor<int, double>
    {
        public ToDouble(Engine.Engine Engine = null) : base("IntegerToDouble", a => a, Engine)
        {

        }
    }

    public sealed class ToString : UnaryFunctor<int, string>
    {
        public ToString(Engine.Engine Engine = null) : base("IntegerToString", a => a.ToString(), Engine)
        {

        }
    }
}