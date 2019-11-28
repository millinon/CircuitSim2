using CircuitSim2;
using CircuitSim2.Chips;
using CircuitSim2.IO;
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
        void PopulateSpawnMenu(ToolStripMenuItem MenuItem, Catalog ChipCatalog) {

            var top_item = new ToolStripMenuItem(ChipCatalog.Name);
            
            foreach(var subcat in ChipCatalog.SubCategories)
            {
                //var new_item = new ToolStripMenuItem(subcat.Name);
                PopulateSpawnMenu(top_item, subcat);

                //top_item.DropDownItems.Add(new_item);
            }

            foreach(var chiptype in ChipCatalog.ChipTypes)
            {
                var new_item = new ToolStripMenuItem(chiptype.Name);

                new_item.Click += (s, e) =>
                {
                    var chip = Activator.CreateInstance(chiptype) as ChipBase;

                    chip.Position = circuitCanvasControl1.Position;
                    chip.Rotation = new RotationVec { Gamma = 45, };

                    //chip.ChildrenVisible = true;

                    foreach(var output in chip.OutputSet.AllOutputs)
                    {
                        output.ValueChanged += (_, __) => circuitCanvasControl1.Refresh();

                    }

                    circuitCanvasControl1.Spawn(chip);
                };

                top_item.DropDownItems.Add(new_item);
            }

            MenuItem.DropDownItems.Add(top_item);
        }

        public Form1()
        {
            InitializeComponent();

            /*var chip = new CircuitSim2.Chips.Digital.Logic.AND()
            {
            };*/

            //var chip = new CircuitSim2.Chips.Byte.Conversion.Compose();

            //var chip = new CircuitSim2.Chips.Components.Adders.FullAdder();
            //chip.Scale = 2;
            //var chip = new CircuitSim2.Chips.Components.Adders.ByteAdder();
            /*chip.Rotation = new CircuitSim2.RotationVec
            {
                Gamma = 45 * Math.PI / 180,
            };*/
            //chip.ChildrenVisible = true;

            //chip.Tick();
            //chip.SuperTick();

            KeyPreview = true;

            var default_catalog = CircuitSim2.Catalog.Default();

            var spawn_menu = spawnToolStripMenuItem;

            PopulateSpawnMenu(spawn_menu, default_catalog);


            SizeChanged += (s, e) => circuitCanvasControl1.Refresh();
//            circuitCanvasControl1.AddChip(chip);

            circuitCanvasControl1.Refresh();

            circuitCanvasControl1.Select();
        }
    }
}
