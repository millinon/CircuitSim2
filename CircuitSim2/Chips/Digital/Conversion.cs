﻿using CircuitSim2.Chips.Functors;

namespace CircuitSim2.Chips.Digital.Conversion
{
    public sealed class ToByte : UnaryFunctor<bool, byte>
    {
        public ToByte(Engine.Engine Engine = null) : this(0, 1, Engine)
        {

        }

        public ToByte(byte Low = 0, byte High = 1, Engine.Engine Engine = null) : base("DigitalToByte", a => a ? High : Low)
        {

        }
    }

    public sealed class ToChar : UnaryFunctor<bool, char>
    {
        public ToChar(char Low, char High, Engine.Engine Engine = null) : base("DigitalToChar", a => a ? High : Low)
        {

        }
    }

    public sealed class ToInteger : UnaryFunctor<bool, int>
    {
        public ToInteger(Engine.Engine Engine = null) : this(0, 1, Engine)
        {

        }

        public ToInteger(int Low = 0, int High = 1, Engine.Engine Engine = null) : base("DigitalToInt", a => a ? High : Low)
        {

        }
    }

    public sealed class ToLong : UnaryFunctor<bool, long>
    {
        public ToLong(Engine.Engine Engine) : this(0L, 1L, Engine)
        {

        }

        public ToLong(long Low = 0L, long High = 1L, Engine.Engine Engine = null) : base("DigitalToLong", a => a ? High : Low)
        {

        }
    }

    public sealed class ToSingle : UnaryFunctor<bool, float>
    {
        public ToSingle(Engine.Engine Engine = null) : this(0.0f, 1.0f, Engine)
        {

        }

        public ToSingle(float Low = 0.0f, float High = 1.0f, Engine.Engine Engine = null) : base("DigitalToSingle", a => a ? High : Low)
        {

        }
    }

    public sealed class ToDouble : UnaryFunctor<bool, double>
    {
        public ToDouble(Engine.Engine Engine = null) : this(0.0, 1.0, Engine)
        {

        }

        public ToDouble(double Low = 0.0, double High = 1.0, Engine.Engine Engine = null) : base("DigitalToDouble", a => a ? High : Low)
        {

        }
    }

    public sealed class ToString : UnaryFunctor<bool, string>
    {
        public ToString(Engine.Engine Engine = null) : this("False", "True", Engine)
        {

        }

        public ToString(string Low = "False", string High = "True", Engine.Engine Engine = null) : base("DigitalToString", a => a ? High : Low)
        {

        }
    }
}
