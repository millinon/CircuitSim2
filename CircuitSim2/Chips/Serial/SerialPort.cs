using CircuitSim2.IO;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace CircuitSim2.Chips.Serial.SerialPort
{
    public sealed class SerialPort : ChipBase
    {
        [NonSerialized]
        private System.IO.Ports.SerialPort port;

        private string portpath;
        [ChipProperty]
        public string PortPath
        {
            get
            {
                return portpath;
            }
            set
            {
                portpath = value;



                try
                {
                    port.Close();
#pragma warning disable CA1031 // Do not catch general exception types
                }
                catch (Exception)
                {

                }
#pragma warning restore CA1031 // Do not catch general exception types

                port.PortName = value;
                port.Open();

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private int baudrate;
        [ChipProperty]
        public int BaudRate
        {
            get { return baudrate; }
            set
            {
                baudrate = value;

                port.BaudRate = value;

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private int readtimeout;
        private int writetimeout;

        [ChipProperty]
        public int ReadTimeout
        {
            get
            {
                return readtimeout;
            }
            set
            {
                readtimeout = value;

                port.ReadTimeout = value;

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        [ChipProperty]
        public int WriteTimeout
        {
            get
            {
                return writetimeout;
            }
            set
            {
                writetimeout = value;

                port.WriteTimeout = value;

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private void mk_port()
        {
            port?.Dispose();

            port = new System.IO.Ports.SerialPort(PortPath, BaudRate)
            {
                ReadTimeout = readtimeout,
                WriteTimeout = writetimeout,
            };

            port.DataReceived += data_received;

            port.Open();
        }

        private void data_received(object sender, SerialDataReceivedEventArgs e)
        {
            var buf = new byte[4096];

            var read_bytes = port.Read(buf, 0, 4096);

            if (read_bytes > 0) {

                Outputs.Out.Value = buf.Take(read_bytes).ToArray();
            }
        }

        [NonSerialized]
        public readonly GenericInput<byte[]> Inputs;

        [NonSerialized]
        public readonly GenericOutput<byte[]> Outputs;

        public SerialPort()
        {
            InputSet = (Inputs = new GenericInput<byte[]>(this));
            OutputSet = (Outputs = new GenericOutput<byte[]>(this));

            port = new System.IO.Ports.SerialPort();
        }

        public override void Compute()
        {
            var data = Inputs.A.Value;

            port.Write(data, 0, data.Length);
        }

        public sealed override void Commit()
        {
        }

        public sealed override void Tick()
        {
        }
    }
}
