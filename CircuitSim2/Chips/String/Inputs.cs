using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSim2.Chips.String.Inputs
{
    [Chip("StringSwitch")]
    [Serializable]
    public sealed class Switch : IO.Inputs.Switch<string>
    {
    }
}
