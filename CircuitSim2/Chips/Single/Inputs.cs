using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSim2.Chips.Single.Inputs
{
    [Chip("SingleSwitch")]
    [Serializable]
    public sealed class Switch : IO.Inputs.Switch<float>
    {
    }
}
