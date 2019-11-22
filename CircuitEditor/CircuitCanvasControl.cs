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
        private readonly HashSet<Wire> Wires;

        public const int SizeScale = 4;
        public const int IORadius = 1;

        private float Zoom = 1.0f;
        public PositionVec Position { get; private set; } = new PositionVec { X = 0, Y = 0, Z = 0, };

        public static readonly Color BackgroundColor = Color.White;

        public bool GridVisible = true;
        public static readonly Color GridColor = Color.LightSlateGray;
        public const int GridWidth = 1;
        public const int GridSpacing = 5;

        public double MinX => -Position.X - (Width / 2) / ScaleFactor;
        public double MaxX => -Position.X + (Width / 2) / ScaleFactor;
        public double MinY => -Position.Y - (Height / 2) / ScaleFactor;
        public double MaxY => -Position.Y + (Height / 2) / ScaleFactor;

        public float ScaleFactor => Zoom * SizeScale;

        private bool Snap = true;
        public enum MouseModes
        {
            None,
            Interact,
            Draw_Wire,
        };

        public MouseModes MouseMode = MouseModes.Draw_Wire;

        private IOBase Closest_IO;

        private ChipBase SpawnedChip;


        private ChipBase GrabbedChip;
        private PositionVec GrabOffset;

        private Wire CurrentWire;



        public void Spawn(ChipBase Chip)
        {
            Chips.Add(SpawnedChip = Chip);
        }

        public readonly Engine Engine;

        public CircuitCanvasControl()
        {
            InitializeComponent();

            Chips = new HashSet<ChipBase>();
            Wires = new HashSet<Wire>();

            Paint += CircuitCanvas_Paint;

            DoubleBuffered = true;

            PreviewKeyDown += CircuitCanvas_PreviewKeydown;

            KeyDown += CircuitCanvasControl_KeyDown;
            KeyUp += CircuitCanvasControl_KeyUp;

            MouseMove += CircuitCanvas_MouseMove;

            MouseClick += CircuitCanvasControl_MouseClick;

            MouseDown += CircuitCanvasControl_MouseDown;
            MouseUp += CircuitCanvasControl_MouseUp;

            Engine = new Engine();
        }

        private double GrabRadius => ScaleFactor / 8;

        private bool PosInChipBounds(ChipBase Chip, PositionVec Position)
        {
            var chip_sz = Chip.Size;
            var chip_pos = Chip.Position;
            var chip_rot = Chip.Rotation;

            //var mouse_pos = closest_chip.Position.Subtract(MousePosition);

            var gamma = -chip_rot.Gamma;

            var rel_pos = new PositionVec
            {
                X = Position.X * Math.Cos(gamma) - Position.Y * Math.Sin(gamma),
                Y = Position.Y * Math.Cos(gamma) + Position.X * Math.Sin(gamma),
                Z = 0,
            };

            var chip_rel_pos = new PositionVec
            {
                X = chip_pos.X * Math.Cos(gamma) - chip_pos.Y * Math.Sin(gamma),
                Y = chip_pos.Y * Math.Cos(gamma) + chip_pos.X * Math.Sin(gamma),
                Z = 0,
            };

            return (rel_pos.X + GrabRadius >= chip_rel_pos.X - chip_sz.Length / 2 &&
                rel_pos.X - GrabRadius <= chip_rel_pos.X + chip_sz.Length / 2 &&
                rel_pos.Y + GrabRadius >= chip_rel_pos.Y - chip_sz.Width / 2 &&
                rel_pos.Y - GrabRadius <= chip_rel_pos.Y + chip_sz.Width / 2);
        }

        private void GrabChip(PositionVec MousePosition)
        {
            var nearby_chips = Chips.Where(chip => InBounds(chip)).OrderBy(chip => Distance(MousePosition, chip.Position));

            if (!nearby_chips.Any())
            {
                GrabbedChip = null;
            }
            else
            {
                var closest_chip = nearby_chips.First();

                if (PosInChipBounds(closest_chip, MousePosition))
                {
                    GrabbedChip = closest_chip;

                    GrabOffset = closest_chip.Position.Subtract(MousePosition);
                }
            }
        }

        private void CircuitCanvasControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GrabChip(LastMousePos);
            }
        }

        private void CircuitCanvasControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GrabbedChip = null;
            }
        }

        PositionVec LastMousePos;

        private void CircuitCanvasControl_KeyDown(object sender, KeyEventArgs e)
        {
            bool invalidate = false;

            if (e.Control)
            {
                Snap = false;

                if (SpawnedChip != null)
                {
                    SpawnedChip.Position = LastMousePos;

                    invalidate = true;
                }
            }



            if (invalidate)
            {
                Refresh();
            }
        }

        private void CircuitCanvasControl_KeyUp(object sender, KeyEventArgs e)
        {
            bool invalidate = false;

            if (!e.Control)
            {
                Snap = true;

                if (SpawnedChip != null)
                {
                    SpawnedChip.Position = SnapToGrid(LastMousePos);

                    invalidate = true;
                }
            }


            if (invalidate)
            {
                Refresh();
            }
        }

        private PositionVec SnapToGrid(PositionVec Position)
        {
            return new PositionVec
            {
                X = Math.Round(Position.X / GridSpacing) * GridSpacing,
                Y = Math.Round(Position.Y / GridSpacing) * GridSpacing,
                Z = Position.Z,
            };
        }

        private Wire.Segment tmp_seg;
        public double JunctionTolerance => ScaleFactor / 4;

        private void CleanupWires()
        {
            var del = new HashSet<Wire>();

            foreach(var wire in Wires)
            {
                if(!wire.Path.Any())
                {
                    del.Add(wire);
                }
            }

            foreach(var wire in Wires)
            {
                var jdel = new HashSet<Wire.Junction>();

                foreach(var junction in wire.Junctions)
                {
                    if(del.Contains(junction.WireA) || del.Contains(junction.WireB))
                    {
                        jdel.Add(junction);
                    }
                }

                foreach(var junction in jdel)
                {
                    wire.Junctions.Remove(junction);
                }
            }

            foreach(var wire in del)
            {
                Wires.Remove(wire);
            }
        }
        
        private void CircuitCanvasControl_MouseClick(object sender, MouseEventArgs e)
        {
            bool invalidate = false;

            PositionVec mouse_pos;
            if (Snap)
            {
                mouse_pos = SnapToGrid(LastMousePos);
            }
            else
            {
                mouse_pos = LastMousePos;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (SpawnedChip != null)
                {
                    SpawnedChip = null;

                    invalidate = true;
                } else if(MouseMode == MouseModes.Draw_Wire)
                {
                    bool new_wire = CurrentWire == null;

                    if (new_wire)
                    {
                        CleanupWires();

                        Wires.Add(CurrentWire = new Wire());
                    }

                    var intersected_wires = Wires.Where(wire => wire != CurrentWire && wire.Intersects(mouse_pos, JunctionTolerance));

                    bool valid_pt;

                    if(CurrentWire.Intersects(mouse_pos, JunctionTolerance))
                    {
                        valid_pt = false;
                    }
                    else if (intersected_wires.Any())
                    {
                        valid_pt = CurrentWire.Join(intersected_wires.First(), mouse_pos);
                    } else
                    {
                        valid_pt = true;
                    }

                    if (valid_pt)
                    {
                        if (new_wire)
                        {
                            if (Closest_IO != null)
                            {
                                if (Closest_IO is InputBase output)
                                {
                                    if (CurrentWire.Attach(output))
                                    {
                                        tmp_seg = new Wire.Segment
                                        {
                                            Output_Start = true,
                                            Output = output,
                                        };
                                    }
                                }
                                else if (Closest_IO is OutputBase input)
                                {
                                    if (CurrentWire.Attach(input))
                                    {
                                        tmp_seg = new Wire.Segment
                                        {
                                            Input_Start = true,
                                            Input = input,
                                        };
                                    }
                                }
                            }
                            else
                            {
                                tmp_seg = new Wire.Segment
                                {
                                    Start = mouse_pos,
                                };
                            }
                        } else
                        {
                            if (Closest_IO != null)
                            {
                                if (Closest_IO is InputBase output)
                                {
                                    if (CurrentWire.Attach(output))
                                    {
                                        tmp_seg.Output_End = true;
                                        tmp_seg.Output = output;
                                    }
                                }
                                else if (Closest_IO is OutputBase input)
                                {
                                    if (CurrentWire.Attach(input))
                                    {
                                        tmp_seg.Input_End = true;
                                        tmp_seg.Input = input;
                                    }
                                }
                            }
                            else
                            {
                                tmp_seg.End = mouse_pos;
                            }

                            CurrentWire.Path.Add(tmp_seg);

                            tmp_seg.Input_Start = false;
                            tmp_seg.Input_Start = false;

                            if (tmp_seg.Input_End)
                            {
                                tmp_seg.Input_Start = true;
                            }
                            else if (tmp_seg.Output_End)
                            {
                                tmp_seg.Output_Start = true;
                            }
                            else
                            {
                                tmp_seg.Start = tmp_seg.End;
                            }
                        }
                    }

                    invalidate = true;
                }
            } else if(e.Button == MouseButtons.Right)
            {
                if(MouseMode == MouseModes.Draw_Wire)
                {
                    CurrentWire = null;

                    CleanupWires();

                    invalidate = true;
                }
            }

            if (invalidate)
            {
                Refresh();
            }
        }

        private void CircuitCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            bool invalidate = false;

            var mousepos = RelativeToAbsolute(e.Location);

            LastMousePos = mousepos;

            if (SpawnedChip != null)
            {
                if (Snap)
                {
                    SpawnedChip.Position = SnapToGrid(mousepos);
                }
                else
                {
                    SpawnedChip.Position = mousepos;
                }

                invalidate = true;
            }
            else if (GrabbedChip != null)
            {
                if (Snap)
                {
                    GrabbedChip.Position = SnapToGrid(mousepos.Add(GrabOffset));
                }
                else
                {
                    GrabbedChip.Position = mousepos.Add(GrabOffset);
                }

                invalidate = true;
            }
            else if (MouseMode == MouseModes.Draw_Wire)
            {
                var AllIOs = new List<IOBase>();
                foreach (var chip in Chips.Where(chip => InBounds(chip)))
                {
                    foreach (var input in chip.InputSet.AllInputs.Where(i => Distance(mousepos, i.Position) <= 5))
                    {
                        AllIOs.Add(input);
                    }

                    foreach (var output in chip.OutputSet.AllOutputs.Where(o => Distance(mousepos, o.Position) <= 5))
                    {
                        AllIOs.Add(output);
                    }
                }

                if (AllIOs.Any())
                {
                    Closest_IO = AllIOs.OrderBy(io => Distance(mousepos, io.Position)).First();

                    invalidate = true;
                }
                else
                {
                    if (Closest_IO != null)
                    {
                        invalidate = true;
                    }

                    Closest_IO = null;
                }

                if(CurrentWire != null)
                {
                    invalidate = true;
                }
            }

            if (invalidate)
            {
                Refresh();
            }
        }

        private void CircuitCanvas_PreviewKeydown(object sender, PreviewKeyDownEventArgs e)
        {
            bool invalidate = false;

            double MoveSpeed = 5 / ScaleFactor;
            double ZoomSpeed = 0.05;

            switch (e.KeyData)
            {
                case Keys.Shift | Keys.Left:
                    MoveSpeed *= 4;
                    goto case Keys.Left;
                case Keys.Left:
                    Position = new PositionVec
                    {
                        X = Position.X + MoveSpeed,
                        Y = Position.Y,
                        Z = 0,
                    };
                    invalidate = true;
                    break;

                case Keys.Shift | Keys.Right:
                    MoveSpeed *= 4;
                    goto case Keys.Right;
                case Keys.Right:
                    Position = new PositionVec
                    {
                        X = Position.X - MoveSpeed,
                        Y = Position.Y,
                        Z = 0,
                    };
                    invalidate = true;
                    break;

                case Keys.Shift | Keys.Up:
                    MoveSpeed *= 4;
                    goto case Keys.Up;
                case Keys.Up:
                    Position = new PositionVec
                    {
                        X = Position.X,
                        Y = Position.Y + MoveSpeed,
                        Z = 0,
                    };
                    invalidate = true;
                    break;

                case Keys.Shift | Keys.Down:
                    MoveSpeed *= 4;
                    goto case Keys.Down;
                case Keys.Down:
                    Position = new PositionVec
                    {
                        X = Position.X,
                        Y = Position.Y - MoveSpeed,
                        Z = 0,
                    };
                    invalidate = true;
                    break;

                case Keys.Shift | Keys.OemMinus:
                    ZoomSpeed *= 4;
                    goto case Keys.OemMinus;

                case Keys.OemMinus:
                    Zoom = (float)Math.Max(0.005, Zoom - ZoomSpeed);
                    invalidate = true;
                    break;

                case Keys.Shift | Keys.Oemplus:
                    ZoomSpeed *= 4;
                    goto case Keys.Oemplus;
                case Keys.Oemplus:
                    Zoom = (float)Math.Min(20.0, Zoom + ZoomSpeed);
                    invalidate = true;
                    break;
            }


            if (invalidate)
            {
                Refresh();
            }
        }

        public static readonly Color Wire_On = Color.Green;
        public static readonly Color Wire_Off = Color.DarkBlue;
        public static readonly Color Wire_Error = Color.Red;
        public Color WireColor(IOBase IO)
        {

            if (IO is OutputBase Output)
            {
                try
                {
                    switch (Output.Type)
                    {
                        case CircuitSim2.IO.Type.DIGITAL:
                            return (Output as Output<bool>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.BYTE:
                            return (Output as Output<byte>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.CHAR:
                            return (Output as Output<char>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.INT:
                            return (Output as Output<int>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.LONG:
                            return (Output as Output<long>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.SINGLE:
                            return (Output as Output<float>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.DOUBLE:
                            return (Output as Output<double>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.STRING:
                            return (Output as Output<string>).Value != default ? Wire_On : Wire_Off;
                        default:
                            return Wire_Error;
                    }
                }
                catch (Exception)
                {
                    return Wire_Error;
                }
            }
            else if (IO is InputBase Input)
            {
                try
                {
                    switch (Input.Type)
                    {
                        case CircuitSim2.IO.Type.DIGITAL:
                            return (Input as Input<bool>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.BYTE:
                            return (Input as Input<byte>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.CHAR:
                            return (Input as Input<char>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.INT:
                            return (Input as Input<int>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.LONG:
                            return (Input as Input<long>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.SINGLE:
                            return (Input as Input<float>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.DOUBLE:
                            return (Input as Input<double>).Value != default ? Wire_On : Wire_Off;
                        case CircuitSim2.IO.Type.STRING:
                            return (Input as Input<string>).Value != default ? Wire_On : Wire_Off;
                        default:
                            return Wire_Error;
                    }
                }
                catch (Exception)
                {
                    return Wire_Error;
                }
            }
            else
            {
                return Wire_Error;
            }
        }

        public bool InBounds(ChipBase Chip)
        {
            var chip_pos = Chip.Position;

            return chip_pos.X >= (MinX - 10 * ScaleFactor) &&
                chip_pos.X <= (MaxX + 10 * ScaleFactor) &&
                chip_pos.Y >= (MinX - 10 * ScaleFactor) &&
                chip_pos.Y <= (MaxY + 10 * ScaleFactor);
        }

        private Point AbsoluteToRelative(PositionVec Position)
        {
            return new Point()
            {
                X = (int)(ScaleFactor * (this.Position.X + Position.X)) + Width / 2,
                Y = (int)(ScaleFactor * (this.Position.Y + Position.Y)) + Height / 2,
            };
        }

        private PositionVec RelativeToAbsolute(Point Point)
        {
            return new PositionVec
            {
                X = (Point.X - Width / 2) / ScaleFactor - Position.X,
                Y = (Point.Y - Height / 2) / ScaleFactor - Position.Y,
                Z = 0,
            };
        }

        /*private Point ChipPos(ChipBase Chip)
        {
            return AbsoluteToRelative(Chip.Position);
        }

        private Point IOPos(CircuitSim2.IO.IOBase IO)
        {
            return AbsoluteToRelative(IO.Position);
        }*/

        private float RadToDeg(double Radians)
        {
            return (float)(Radians * (180 / Math.PI));
        }

        private double Distance(PositionVec A, PositionVec B)
        {
            return Math.Sqrt(Math.Pow(A.X - B.X, 2.0) + Math.Pow(A.Y - B.Y, 2.0));
        }

        private void DrawGrid(Graphics Graphics)
        {
            if (!GridVisible)
            {
                return;
            }

            using (var gridline_pen = new Pen(GridColor)
            {
                Width = GridWidth,
                Alignment = System.Drawing.Drawing2D.PenAlignment.Center,
            })
            {

                //double x_start;
                var x_start = MinX + ((-MinX) % GridSpacing);
                var num_x_lines = ((MaxX - MinX) / GridSpacing) + 1;

                for (int i = 0; i < num_x_lines; i++)
                {
                    Graphics.DrawLine(gridline_pen, AbsoluteToRelative(new PositionVec()
                    {
                        X = x_start + GridSpacing * i,
                        Y = MinY,
                    }), AbsoluteToRelative(new PositionVec()
                    {
                        X = x_start + GridSpacing * i,
                        Y = MaxY,
                    }));
                }

                var y_start = MinY + ((-MinY) % GridSpacing);
                var num_y_lines = ((MaxY - MinY) / GridSpacing) + 1;

                for (int i = 0; i < num_y_lines; i++)
                {
                    Graphics.DrawLine(gridline_pen, AbsoluteToRelative(new PositionVec()
                    {
                        X = MinX,
                        Y = y_start + GridSpacing * i,
                    }), AbsoluteToRelative(new PositionVec()
                    {
                        X = MaxX,
                        Y = y_start + GridSpacing * i,
                    }));
                }
            }
        }

        private void DrawIO(Graphics Graphics, IOBase IO, Pen Pen, bool Label)
        {
            var scaled_io_radius = (int)(ScaleFactor * IO.Chip.Scale * IORadius);

            if (scaled_io_radius > 0)
            {
                var io_pt = AbsoluteToRelative(IO.Position);

                var chip_rot = IO.Chip.Rotation;

                Graphics.DrawArc(Pen, new Rectangle()
                {
                    X = io_pt.X - scaled_io_radius,
                    Y = io_pt.Y - scaled_io_radius,

                    Width = scaled_io_radius * 2,
                    Height = scaled_io_radius * 2,
                }, RadToDeg(chip_rot.Gamma) + (IO is InputBase ? -90 : 90), 180);

                if (Label)
                {
                    Graphics.TranslateTransform(io_pt.X, io_pt.Y);
                    Graphics.RotateTransform(RadToDeg(chip_rot.Gamma));
                    Graphics.DrawString(IO.Name, new Font("Consolas", (float)(ScaleFactor * IO.Chip.Scale * 0.5)), Brushes.Black, (float)(ScaleFactor * IO.Chip.Scale * (IO is InputBase ? 1.5 : -1.5)), (float)(ScaleFactor * IO.Chip.Scale * 1), new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                    });
                    Graphics.ResetTransform();
                }
            }
        }
        private void DrawChip(Graphics Graphics, CircuitSim2.Chips.ChipBase Chip, bool LabelIOs)
        {
            if (!Chip.Visible)
            {
                return;
            }


            var subwire_width = Chip.Scale / 1.5;
            var chip_rot = Chip.Rotation;

            var chip_sz = Chip.Size;
            var chip_pos = AbsoluteToRelative(Chip.Position);//ChipPos(Chip);
            Graphics.TranslateTransform(chip_pos.X, chip_pos.Y);
            Graphics.RotateTransform(RadToDeg(chip_rot.Gamma));

            Graphics.FillRectangle(Brushes.LightGray, new Rectangle()
            {
                X = (int)-(ScaleFactor * (chip_sz.Length / 2)),
                Y = (int)-(ScaleFactor * (chip_sz.Width / 2)),

                Width = (int)Math.Ceiling(ScaleFactor * chip_sz.Length),
                Height = (int)Math.Ceiling(ScaleFactor * chip_sz.Width),
            });
            Graphics.DrawRectangle(new Pen(Color.Black)
            {
                Alignment = System.Drawing.Drawing2D.PenAlignment.Outset,
                Width = (float)Chip.Scale * 2,
            }, new Rectangle()
            {
                X = (int)-(ScaleFactor * (chip_sz.Length / 2)),
                Y = (int)-(ScaleFactor * (chip_sz.Width / 2)),

                Width = (int)Math.Ceiling(ScaleFactor * chip_sz.Length),
                Height = (int)Math.Ceiling(ScaleFactor * chip_sz.Width),
            }); ;

            Graphics.ResetTransform();

            using (var IO_pen = new Pen(Color.Black)
            {
                Alignment = System.Drawing.Drawing2D.PenAlignment.Inset,
                Width = (float)(Chip.Scale),
            })
            {
                foreach (var input in Chip.InputSet.AllInputs)
                {
                    DrawIO(Graphics, input, IO_pen, LabelIOs);
                }

                foreach (var output in Chip.OutputSet.AllOutputs)
                {
                    DrawIO(Graphics, output, IO_pen, LabelIOs);
                }
            }

            if (Chip.ChildrenVisible)
            {
                foreach (var subchip in Chip.SubChips)
                {
                    DrawChip(Graphics, subchip, false);

                    foreach (var input in subchip.InputSet.AllInputs.Where(input => input.IsAttached))
                    {
                        Graphics.DrawLine(new Pen(WireColor(input.SourceBase))
                        {
                            Alignment = System.Drawing.Drawing2D.PenAlignment.Center,
                            Width = (float)subwire_width,
                        }, AbsoluteToRelative(input.Position), AbsoluteToRelative(input.SourceBase.Position));
                    }

                    foreach (var output in subchip.OutputSet.AllOutputs.Where(output => output.Sinks().Any()))
                    {
                        foreach (var sink in output.Sinks())
                        {
                            Graphics.DrawLine(new Pen(WireColor(output))
                            {
                                Alignment = System.Drawing.Drawing2D.PenAlignment.Center,
                                Width = (float)subwire_width,
                            }, AbsoluteToRelative(output.Position), AbsoluteToRelative(sink.Position));
                        }
                    }
                }

                foreach (var input in Chip.InputSet.AllInputs)
                {
                    foreach (var sub_input in input.Hooks)
                    {
                        Graphics.DrawLine(new Pen(WireColor(input.SourceBase))
                        {
                            Alignment = System.Drawing.Drawing2D.PenAlignment.Center,
                            Width = (float)subwire_width,
                        }, AbsoluteToRelative(input.Position), AbsoluteToRelative(sub_input.Position));
                    }
                }

                foreach (var output in Chip.OutputSet.AllOutputs)
                {
                    if (output.IsBound)
                    {
                        var bound_output = output.Binding;
                        var bound_pos = bound_output.Position;

                        Graphics.DrawLine(new Pen(WireColor(output))
                        {
                            Alignment = System.Drawing.Drawing2D.PenAlignment.Center,
                            Width = (float)subwire_width,
                        }, AbsoluteToRelative(output.Position), AbsoluteToRelative(bound_output.Position));
                    }
                }


            }
            else
            {
                Graphics.TranslateTransform(chip_pos.X, chip_pos.Y);
                Graphics.RotateTransform(RadToDeg(chip_rot.Gamma) + 90);
                Graphics.DrawString(Chip.Name, new Font("Consolas", (float)(ScaleFactor * Chip.Scale)), Brushes.Black, 0, 0, new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                });
                Graphics.ResetTransform();
            }
        }

        private void DrawWire(Graphics Graphics, Wire Wire)
        {
            var wire_info = Wire.Render();

            Color wire_color;
            if(wire_info.Input == null)
            {
                wire_color = Wire_Error;
            } else
            {
                wire_color = WireColor(wire_info.Input);
            }

            using (var pen = new Pen(wire_color)
            {
                Width = (float)ScaleFactor / 2,
                Alignment = System.Drawing.Drawing2D.PenAlignment.Center,
            })
            {
                foreach(var seg in Wire.Path)
                {
                    PositionVec seg_start;
                    if (seg.Input_Start)
                    {
                        seg_start = seg.Input.Position;
                    } else if (seg.Output_Start)
                    {
                        seg_start = seg.Output.Position;
                    } else
                    {
                        seg_start = seg.Start;
                    }

                    PositionVec seg_end;
                    if (seg.Input_End)
                    {
                        seg_end = seg.Output.Position;
                    } else if (seg.Output_End) {
                        seg_end = seg.Output.Position;
                    } else
                    {
                        seg_end = seg.End;
                    }

                    Graphics.DrawLine(pen, AbsoluteToRelative(seg_start), AbsoluteToRelative(seg_end));
                }

                foreach(var junction in Wire.Junctions){
                    
                    var junction_pt = AbsoluteToRelative(junction.Position);

                    var width = ScaleFactor;

                    Graphics.FillEllipse(new SolidBrush(wire_color), new Rectangle
                    {
                        X = (int)(junction_pt.X - ScaleFactor),
                        Y = (int)(junction_pt.Y - ScaleFactor),

                        Width = (int)ScaleFactor*2,
                        Height = (int)ScaleFactor*2,
                    });

                }
            }
        }

        private void DrawWires(Graphics Graphics)
        {
            foreach(var wire in Wires)
            {
                DrawWire(Graphics, wire);
            }

            if (MouseMode == MouseModes.Draw_Wire && CurrentWire != null)
            {
                var info = CurrentWire.Render();

                Color wire_color;
                if(info.Input != null)
                {
                    wire_color = WireColor(info.Input);
                } else
                {
                    wire_color = Wire_Error;
                }

                using (var pen = new Pen(wire_color)
                {
                    Width = (float)ScaleFactor / 2,
                    Alignment = System.Drawing.Drawing2D.PenAlignment.Center,
                })
                {
                    PositionVec seg_start;
                    if (tmp_seg.Input_Start)
                    {
                        seg_start = tmp_seg.Input.Position;
                    } else if (tmp_seg.Output_Start)
                    {
                        seg_start = tmp_seg.Output.Position;
                    } else
                    {
                        seg_start = tmp_seg.Start;
                    }

                    PositionVec seg_end;
                    if (Snap)
                    {
                        seg_end = SnapToGrid(LastMousePos);
                    } else
                    {
                        seg_end = LastMousePos;
                    }

                    Graphics.DrawLine(pen, AbsoluteToRelative(seg_start), AbsoluteToRelative(seg_end));
                }
            }
        }

        private void CircuitCanvas_Paint(object sender, PaintEventArgs e)
        {
            var Graphics = e.Graphics;

            Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Graphics.Clear(BackgroundColor);

            DrawGrid(Graphics);

            foreach (var chip in Chips.Where(chip => InBounds(chip)))
            {
                DrawChip(Graphics, chip, true);
            }

            if (Closest_IO != null)
            {
                using (var pen = new Pen(Color.Yellow)
                {
                    Alignment = System.Drawing.Drawing2D.PenAlignment.Inset,
                    Width = (float)(Closest_IO.Chip.Scale * 2),
                })
                {
                    DrawIO(Graphics, Closest_IO, pen, false);
                }
            }

            DrawWires(Graphics);
        }

        private class Wire
        {
            public class Junction
            {
                public PositionVec Position;

                public Wire WireA;
                public Wire WireB;
            }

            public struct Segment
            {
                public InputBase Output;
                public bool Output_Start;
                public bool Output_End;

                public OutputBase Input;
                public bool Input_Start;
                public bool Input_End;

                public PositionVec Start;
                public PositionVec End;
            }

            public struct ConnectionInfo
            {
                public OutputBase Input;
                public IEnumerable<InputBase> Outputs;
            }

            public ConnectionInfo Render()
            {
                var visited_wires = new HashSet<Wire>
                {
                    this
                };

                void visit_junction(Junction J)
                {
                    if(!visited_wires.Contains(J.WireA))
                    {
                        visited_wires.Add(J.WireA);
                        
                        foreach(var junction in J.WireA.Junctions)
                        {
                            visit_junction(junction);
                        }
                    }

                    if (!visited_wires.Contains(J.WireB))
                    {
                        visited_wires.Add(J.WireB);

                        foreach(var junction in J.WireB.Junctions)
                        {
                            visit_junction(junction);
                        }
                    }
                }

                foreach(var junction in Junctions)
                {
                    visit_junction(junction);
                }

                var outputs = new HashSet<InputBase>();

                foreach(var wire in visited_wires)
                {
                    foreach(var output in wire.Outputs)
                    {
                        outputs.Add(output);
                    }
                }

                OutputBase input = null;
                if(Input != null)
                {
                    input = Input;
                } else
                {
                    var attached_wires = visited_wires.Where(wire => wire.Input != null);

                    if(attached_wires.Any())
                    {
                        input = attached_wires.First().Input;
                    }
                }

                return new ConnectionInfo
                {
                    Input = input,
                    Outputs = outputs,
                };
            }

            public void Connect()
            {
                var info = Render();

                foreach(var output in info.Outputs)
                {
                    output.Detach();
                }

                if(info.Input != null)
                {
                    foreach(var output in info.Outputs)
                    {
                        output.Attach(info.Input);
                    }
                }
            }

            public void Disconnect(Wire wire)
            {
                var del_junctions = new List<Junction>();

                foreach(var junction in Junctions.Where(j => j.WireA == wire || j.WireB == wire))
                {
                    del_junctions.Add(junction);
                }

                foreach(var junction in del_junctions)
                {
                    Junctions.Remove(junction);
                }
            }

            public void Disconnect()
            {
                var info = Render();

                foreach(var output in info.Outputs)
                {
                    output.Detach();
                }

                foreach(var junction in Junctions)
                {
                    if(this == junction.WireA)
                    {
                        junction.WireB.Disconnect(this);
                    } else
                    {
                        junction.WireA.Disconnect(this);
                    }
                }
            }

            private OutputBase Input;

            public bool Attach(OutputBase Input)
            {
                if(this.Input != null)
                {
                    return false;
                }
                else if(Outputs.Any(output => output.Type != Input.Type))
                {
                    return false;
                } else
                {
                    this.Input = Input;
                    return true;
                }
            }

            private List<InputBase> Outputs;
            public bool Attach(InputBase Output)
            {
                if (Outputs.Contains(Output))
                {
                    return false;
                } else if(Input != null && Input.Type != Output.Type)
                {
                    return false;
                } else
                {
                    Outputs.Add(Output);
                    return true;
                }
            }

            public bool Join(Wire Wire, PositionVec Position)
            {
                var joined_wires = new HashSet<Wire>();

                void visit_wire(Wire joined_wire)
                {
                    if(!joined_wires.Contains(joined_wire))
                    {
                        joined_wires.Add(joined_wire);

                        foreach(var junction in joined_wire.Junctions)
                        {
                            if(joined_wire != junction.WireA)
                            {
                                visit_wire(junction.WireA);
                            } else
                            {
                                visit_wire(junction.WireB);
                            }
                        }
                    }
                }

                visit_wire(Wire);

                if (joined_wires.Contains(this))
                {
                    return false;
                } else if(Input != null && joined_wires.Any(wire => wire.Input != null))
                {
                    return false;
                }

                var new_junction = new Junction
                {
                    WireA = this,
                    WireB = Wire,
                    Position = Position,
                };

                Junctions.Add(new_junction);
                Wire.Junctions.Add(new_junction);

                return true;
            }

            public List<Junction> Junctions;

            public bool Intersects(PositionVec Position, double Tolerance)
            {
                foreach(var seg in Path) {
                    PositionVec seg_start;
                    if (seg.Input_Start)
                    {
                        seg_start = seg.Input.Position;
                    }
                    else if (seg.Output_Start)
                    {
                        seg_start = seg.Output.Position;
                    }
                    else
                    {
                        seg_start = seg.Start;
                    }

                    PositionVec seg_end;
                    if (seg.Input_End)
                    {
                        seg_end = seg.Output.Position;
                    }
                    else if (seg.Output_End)
                    {
                        seg_end = seg.Output.Position;
                    }
                    else
                    {
                        seg_end = seg.End;
                    }


                    //var distance = Math.Abs((seg_end.Y - seg_start.Y) * Position.X - (seg_end.X - seg_start.X) * Position.Y + seg_end.X * seg_start.Y - seg_end.Y * seg_start.X) /
                    //    Math.Sqrt(Math.Pow(seg_end.Y - seg_start.Y, 2.0) + Math.Pow(seg_end.X - seg_start.X, 2.0));

                    var start_distance = Math.Sqrt(Math.Pow(seg_start.X - Position.X, 2.0) + Math.Pow(seg_start.Y - Position.Y, 2.0));
                    var end_distance = Math.Sqrt(Math.Pow(seg_end.X - Position.X, 2.0) + Math.Pow(seg_end.Y - Position.Y, 2.0));

                    var distance = Math.Min(start_distance, end_distance);

                    if(distance <= Tolerance)
                    {
                        return true;
                    }
                }

                return false;
            }

            public List<Segment> Path;

            public Wire()
            {
                Outputs = new List<InputBase>();
                Junctions = new List<Junction>();
                Path = new List<Segment>();
            }
        }

    }
}
