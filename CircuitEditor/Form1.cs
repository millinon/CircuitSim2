using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircuitEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            /*var chip = new CircuitSim2.Chips.Digital.Logic.AND()
            {
            };*/

            //var chip = new CircuitSim2.Chips.Byte.Conversion.Compose();

            //var chip = new CircuitSim2.Chips.Components.Adders.FullAdder();
            //chip.Scale = 2;
            var chip = new CircuitSim2.Chips.Components.Adders.ByteAdder();
            /*chip.Rotation = new CircuitSim2.RotationVec
            {
                Gamma = 45 * Math.PI / 180,
            };*/
            chip.ChildrenVisible = true;

            circuitCanvasControl1.AddChip(chip);

            circuitCanvasControl1.Refresh();
        }
    }
}
