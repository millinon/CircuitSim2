﻿using System;

using CircuitSim2.Engine;
using CircuitSim2.Chips.Time;

namespace Time
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Engine Engine = new Engine())
            {
                var clock = new RealTimeClock()
                {
                    Engine = Engine,
                    Period = TimeSpan.FromSeconds(5),
                };
                var tickcount = new TickCount()
                {
                    Engine = Engine,
                };

                clock.Outputs.Out.ValueChanged += (s, e) =>
                {
                    if (clock.Outputs.Out.Value)
                    {
                        tickcount.Tick();
                        Console.WriteLine($"TickCount = {tickcount.Outputs.Out.Value}");
                    }
                };

                Engine.Start(1000);

                Console.WriteLine("Press Enter to end the program...");
                Console.ReadLine();
            }
        }
    }
}
