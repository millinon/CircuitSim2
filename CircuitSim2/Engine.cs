using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace CircuitSim2.Engine
{
    public class Engine : IDisposable
    {
        private readonly Timer Timer;

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

                        foreach(var depset in Parents.Select(parent => parent.Dependencies))
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

                if (Chips.Count() > 0 && !Roots.Any())
                {
                    throw new Exception("Cyclical circuit detected");
                }
            }
        }

        private readonly Dictionary<string, Chips.ChipBase> Chips;

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

        public Engine()
        {
            lock_obj = new object();

            Timer = new Timer();
            Timer.Elapsed += (s, e) =>
            {
                this.UpdateNext();
            };
            Timer.AutoReset = true;

            Chips = new Dictionary<string, Chips.ChipBase>();
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
                //Graph = new DependencyGraph(Chips.Values);
            }
        }

        public void Unregister(Chips.ChipBase Chip)
        {
            lock (lock_obj)
            {
                if (Chips.ContainsKey(Chip.ID)) Chips.Remove(Chip.ID);

                //Graph = new DependencyGraph(Chips.Values);
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

        public void UpdateNext()
        {
            lock (Updates)
            {
                while (Updates.Size > 0)
                {
                    var chip = Updates.Pop();

                    if(chip.IsPure && Updates.Contains(chip))
                    {
                        ChipSkipped?.Invoke(this, new SkipEventArgs { Chip = chip });
                    } else
                    {
                        chip.Tick();

                        ChipUpdated?.Invoke(this, new UpdateEventArgs { Chip = chip });

                        return;
                    }
                }
            }
        }

        public void ScheduleUpdate(Chips.ChipBase Chip)
        {
            lock (Updates)
            {
                Updates.Push(Chip);
            }
        }

        public void Start(int period_ms = 10)
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
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    lock (lock_obj)
                    {
                        if (Timer.Enabled) Timer.Stop();
                    }
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

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