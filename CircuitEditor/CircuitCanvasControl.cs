using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CircuitSim2;
using CircuitSim2.Engine;
using CircuitSim2.Chips;
using CircuitSim2.IO;

namespace CircuitEditor
{
    public partial class CircuitCanvasControl : UserControl
    {
        private readonly HashSet<ChipBase> Chips;

        private PositionVec Position;

        public const int SizeScale = 8;
        public const int IORadius = 8;
        public void AddChip(ChipBase Chip)
        {
            Chips.Add(Chip);
        }

        public void RemoveChip(ChipBase Chip)
        {
            Chips.Remove(Chip);
        }

        public CircuitCanvasControl()
        {
            InitializeComponent();

            Chips = new HashSet<ChipBase>();

            Paint += CircuitCanvas_Paint;

            DoubleBuffered = true;
        }


        private Point ChipPos(ChipBase Chip)
        {
            var chip_pos = Chip.Position;

            return new Point()
            {
                X = (int)(SizeScale*(Position.X + chip_pos.X)) + Width / 2,
                Y = (int)(SizeScale*(Position.Y + chip_pos.Y)) + Height / 2,
            };
        }

        private Point IOPos(ChipBase Chip, CircuitSim2.IO.IOBase IO)
        {
            var io_pos = IO.Position;

            return new Point()
            {
                X = (int)(SizeScale * ((Position.X + io_pos.X))) + Width / 2,
                Y = (int)(SizeScale * ((Position.Y + io_pos.Y))) + Height / 2,
            };
        }

        private float RadToDeg(double Radians)
        {
            return (float)(Radians * (180 / Math.PI));
        }

        private void DrawChip(Graphics Graphics, CircuitSim2.Chips.ChipBase Chip)
        {
            var scaled_io_radius = (int)(IORadius * Chip.Scale);
            var subwire_width = Chip.Scale / 1.5;
            var chip_rot = Chip.Rotation;

            foreach (var input in Chip.InputSet.AllInputs)
            {
                var input_pt = IOPos(Chip, input);
                
                Graphics.DrawArc(new Pen(Color.Black)
                {
                    Alignment = System.Drawing.Drawing2D.PenAlignment.Inset,
                    Width = (float)(Chip.Scale),
                }, new Rectangle()
                {
                    X = input_pt.X - scaled_io_radius,
                    Y = input_pt.Y - scaled_io_radius,

                    Width = scaled_io_radius * 2,
                    Height = scaled_io_radius * 2,
                }, RadToDeg(chip_rot.Gamma) - 90, 180);

                if (Chip.ChildrenVisible)
                {
                    foreach(var sub_input in input.Hooks)
                    {
                        Graphics.DrawLine(new Pen(Color.Black)
                        {
                            Alignment = System.Drawing.Drawing2D.PenAlignment.Center,
                            Width = (float)subwire_width,
                        }, IOPos(Chip, input), IOPos(sub_input.Chip, sub_input));
                    }
                }
            }

            foreach (var output in Chip.OutputSet.AllOutputs)
            {
                var output_pt = IOPos(Chip, output);

                Graphics.DrawArc(new Pen(Color.Black)
                {
                    Alignment = System.Drawing.Drawing2D.PenAlignment.Inset,
                    Width = (float)(Chip.Scale),
                }, new Rectangle()
                {
                    X = output_pt.X - scaled_io_radius,
                    Y = output_pt.Y - scaled_io_radius,

                    Width = scaled_io_radius * 2,
                    Height = scaled_io_radius * 2,
                }, RadToDeg(chip_rot.Gamma) + 90, 180);

                if (Chip.ChildrenVisible && output.IsBound)
                {
                    var bound_output = output.Binding;
                    var bound_pos = bound_output.Position;

                    Graphics.DrawLine(new Pen(Color.Black)
                    {
                        Alignment = System.Drawing.Drawing2D.PenAlignment.Center,
                        Width = (float) subwire_width,
                    }, IOPos(Chip, output), IOPos(bound_output.Chip, bound_output));
                }
            }

            var chip_sz = Chip.Size;
            var chip_pos = ChipPos(Chip);
            Graphics.TranslateTransform(chip_pos.X, chip_pos.Y);
            Graphics.RotateTransform(RadToDeg(chip_rot.Gamma));
            Graphics.DrawRectangle(new Pen(Color.Black)
            {
                Alignment = System.Drawing.Drawing2D.PenAlignment.Outset,
                Width = (float) Chip.Scale * 2,
            }, new Rectangle()
            {
                X = (int)(- (SizeScale * (chip_sz.Length/2))),
                Y = (int)(- (SizeScale * (chip_sz.Width/2))),

                Width = (int)Math.Ceiling(chip_sz.Length * SizeScale),
                Height = (int)Math.Ceiling(chip_sz.Width * SizeScale),
            });;
            Graphics.ResetTransform();

            if (Chip.ChildrenVisible)
            {
                foreach (var subchip in Chip.SubChips)
                {
                    DrawChip(Graphics, subchip);

                    foreach(var input in subchip.InputSet.AllInputs.Where(input => input.IsAttached))
                    {
                        Graphics.DrawLine(new Pen(Color.Black)
                        {
                            Alignment = System.Drawing.Drawing2D.PenAlignment.Center,
                            Width = (float)subwire_width,
                        }, IOPos(subchip, input), IOPos(input.SourceBase.Chip, input.SourceBase));
                    }

                    foreach(var output in subchip.OutputSet.AllOutputs.Where(output => output.Sinks().Any())){
                        foreach(var sink in output.Sinks())
                        {
                            Graphics.DrawLine(new Pen(Color.Black)
                            {
                                Alignment = System.Drawing.Drawing2D.PenAlignment.Center,
                                Width = (float)subwire_width,
                            }, IOPos(subchip, output), IOPos(sink.Chip, sink));
                        }
                    }
                }
            }
        }


        private void CircuitCanvas_Paint(object sender, PaintEventArgs e)
        {
            var Graphics = e.Graphics;

            foreach (var chip in Chips)
            {
                DrawChip(Graphics, chip);
            }
        }
    }
}
