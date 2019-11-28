using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSim2.Chips.Long.Inputs
{
    [Chip("LongSwitch")]
    [Serializable]
    public sealed class Switch : IO.Inputs.Switch<long>
    {
    }
}
