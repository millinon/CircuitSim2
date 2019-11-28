using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSim2.Chips.Integer.Inputs
{
    [Chip("IntegerSwitch")]
    [Serializable]
    public sealed class Switch : IO.Inputs.Switch<int>
    {
    }
}
