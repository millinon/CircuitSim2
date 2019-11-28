using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSim2.Chips.Double.Inputs
{
    [Chip("DoubleSwitch")]
    [Serializable]
    public sealed class Switch : IO.Inputs.Switch<double>
    {
    }
}
