using CircuitSim2.Chips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;

namespace CircuitSim2.Engine
{
    public class Engine : IDisposable
    {
        private readonly System.Timers.Timer Timer;

        private class DependencyGraph
        {
            protected Dictionary<Chips.ChipBase, Node> map;

            public class Node
            {
                public IEnumerable<Node> Parents;
                public IEnumerable<Node> Children;
                public Chips.ChipBase Chip;

                public Node(Chips.ChipBase Chip)
                {
                    this.Chip = Chip;
                }

                public IEnumerable<Node> Dependencies
                {
                    get
                    {
                        var deps = new HashSet<Node>(Parents);

                        foreach (var depset in Parents.Select(parent => parent.Dependencies))
                        {
                            deps.UnionWith(depset);
                        }

                        return deps;
                    }
                }

                public IEnumerable<Node> Dependents
                {
                    get
                    {
                        var deps = new HashSet<Node>(Children);

                        foreach (var depset in Children.Select(child => child.Dependents))
                        {
                            deps.UnionWith(depset);
                        }

                        return deps;
                    }
                }
            }

            public IEnumerable<Node> Roots
            {
                get
                {
                    return map.Values.Where(node => node.Parents.Count() == 0);
                }
            }

            public IEnumerable<Node> Leaves
            {
                get
                {
                    return map.Values.Where(node => node.Children.Count() == 0);
                }
            }

            public DependencyGraph(IEnumerable<Chips.ChipBase> Chips)
            {
                map = new Dictionary<Chips.ChipBase, Node>();

                foreach (var chip in Chips)
                {
                    map[chip] = new Node(chip);
                }

                foreach (var chip in Chips)
                {
                    var node = map[chip];

                    node.Parents = node.Chip.InputSet.AllInputs.Where(input => input.IsAttached).Select(input => map[input.SourceBase.Chip]);

                    var sinks = new HashSet<Chips.ChipBase>();

                    void add(IO.InputBase input)
                    {
                        if (sinks.Contains(input.Chip)) return;

                        foreach (var hook in input.Hooks)
                        {
                            add(hook);
                        }

                        sinks.Add(input.Chip);
                    }

                    foreach (var output in chip.OutputSet.AllOutputs)
                    {
                        foreach (var sink in output.Sinks())
                        {
                            add(sink);
                        }
                    }

                    node.Children = sinks.Select(sink => map[sink]);
                }
            }

            public Node this[CircuitSim2.Chips.ChipBase Chip]
            {
                get
                {
                    return map[Chip];
                }
            }
        }

        private readonly Dictionary<string, Chips.ChipBase> Chips;
        private readonly Dictionary<string, Chips.Time.Clock> Clocks;

        private DependencyGraph Graph;

        public class UpdateQueue
        {
            private readonly Queue<Chips.ChipBase> Queue;

            public UpdateQueue()
            {
                Queue = new Queue<Chips.ChipBase>();
            }

            public void Push(Chips.ChipBase Chip)
            {
                lock (Queue)
                {
                    Queue.Enqueue(Chip);
                }
            }

            public Chips.ChipBase Pop()
            {
                lock (Queue)
                {
                    if (Queue.Count == 0) throw new InvalidOperationException();

                    return Queue.Dequeue();
                }
            }

            public int Size
            {
                get
                {
                    lock (Queue)
                    {
                        return Queue.Count();
                    }
                }
            }

            public bool Contains(Chips.ChipBase Chip)
            {
                lock (Queue)
                {
                    return Queue.Contains(Chip);
                }
            }
        }

        private UpdateQueue Updates;

        private readonly object lock_obj;

        public IEnumerable<ChipBase> AllChips => Chips.Values;

        public Engine()
        {
            lock_obj = new object();

            Timer = new System.Timers.Timer();
            Timer.Elapsed += (s, e) =>
            {
                UpdateNext();
                UpdateClocks();
            };
            Timer.AutoReset = true;

            Chips = new Dictionary<string, Chips.ChipBase>();
            Clocks = new Dictionary<string, Chips.Time.Clock>();
            Updates = new UpdateQueue();
        }

        public void RegenerateGraph()
        {
            lock (lock_obj)
            {
                Graph = new DependencyGraph(Chips.Values);
            }
        }

        public void Register(Chips.ChipBase Chip)
        {
            lock (lock_obj)
            {
                Chips[Chip.ID] = Chip;

                if (Chip is Chips.Time.Clock ClockChip)
                {
                    Clocks[Chip.ID] = ClockChip;
                }
            }
        }

        public void Unregister(Chips.ChipBase Chip)
        {
            lock (lock_obj)
            {
                if (Chips.ContainsKey(Chip.ID)) Chips.Remove(Chip.ID);

                if (Chip is Chips.Time.Clock ClockChip)
                {
                    Clocks.Remove(Chip.ID);
                }
            }
        }

        public class SkipEventArgs : EventArgs
        {
            public Chips.ChipBase Chip;
        }

        public event EventHandler<SkipEventArgs> ChipSkipped;

        public class UpdateEventArgs : EventArgs
        {
            public Chips.ChipBase Chip;
        }

        public event EventHandler<UpdateEventArgs> ChipUpdated;

        public void UpdateClocks()
        {
            lock (lock_obj)
            {
                foreach (var Clock in Clocks.Values)
                {
                    Clock.Update();
                }
            }
        }

        public void UpdateNext()
        {
            ChipBase chip = null;

            lock (Updates)
            {
                while (Updates.Size > 0)
                {
                    chip = Updates.Pop();
                    var node = Graph[chip];

                    if (chip.IsPure && Updates.Contains(chip) && !node.Children.Contains(node))
                    {
                        ChipSkipped?.Invoke(this, new SkipEventArgs { Chip = chip });
                    }
                    else break;
                }
            }

            if (chip != null)
            {
                chip.Tick();

                ChipUpdated?.Invoke(this, new UpdateEventArgs { Chip = chip });
            }
        }

        public void FlushAll()
        {
            int size;
            do
            {
                size = Updates.Size;

                if (size > 0)
                {
                    UpdateNext();
                }
            } while (size > 0);
        }

        public void Flush(int MaxChips)
        {
            int size;
            do
            {
                size = Updates.Size;

                if (size > 0)
                {
                    UpdateNext();
                }
            } while (size > 0 && --MaxChips > 0);
        }

        public void ScheduleUpdate(Chips.ChipBase Chip)
        {
            lock (Updates)
            {
                Updates.Push(Chip);
            }
        }

        public void Wait()
        {
            int size;

            do
            {
                lock (Updates)
                {
                    size = Updates.Size;

                    if (size > 0) Thread.Sleep(100);
                }
            } while (size > 0);
        }

        public void Start(double period_ms = 10.0)
        {
            lock (lock_obj)
            {
                Timer.Interval = period_ms;

                Timer.Start();
            }
        }

        public void Stop()
        {
            lock (lock_obj)
            {
                Timer.Stop();
            }
        }



        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    lock (lock_obj)
                    {
                        if (Timer != null)
                        {
                            if (Timer.Enabled)
                            {
                                Timer.Stop();
                            }
                            Timer.Dispose();
                        }
                        foreach (var chip in Chips.Values)
                        {
                            chip.Dispose();
                        }
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}