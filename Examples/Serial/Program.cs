using CircuitSim2.Chips.Byte.Generators;
using CircuitSim2.Chips.Serial.SerialPort;
using System;
using System.Text;
using System.Threading;

namespace Serial
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("missing serial port path");
                return;
            }

            var input = new CircuitSim2.Chips.Functors.Constant<byte[]>();

            var serialport = new SerialPort()
            {
                PortPath = args[0],
                BaudRate = 9600,
            };

            serialport.Inputs.A.Attach(input.Outputs.Out);
            serialport.Outputs.Out.ValueChanged += (s, e) =>
            {
                Console.WriteLine($"received: {Encoding.UTF8.GetString(serialport.Outputs.Out.Value)}");
            };

            string line;
            while((line = Console.ReadLine()) != null)
            {
                input.Value = Encoding.UTF8.GetBytes(line);
                Thread.Sleep(100);
            }


        }
    }
}
