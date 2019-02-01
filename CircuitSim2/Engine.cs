using System;
using System.Collections.Generic;
using System.Linq;

namespace CircuitSim2.Engine
{
    public class Engine
    {
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

        private readonly HashSet<Chips.ChipBase> Chips;

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

            Chips = new HashSet<Chips.ChipBase>();
            Updates = new UpdateQueue();
        }

        public void RegenerateGraph()
        {
            lock (lock_obj)
            {
                Graph = new DependencyGraph(Chips);
            }
        }

        public void Register(Chips.ChipBase Chip)
        {
            lock (lock_obj)
            {
                Chips.Add(Chip);
                Graph = new DependencyGraph(Chips);
            }
        }

        public void Unregister(Chips.ChipBase Chip)
        {
            lock (lock_obj)
            {
                if (Chips.Contains(Chip)) Chips.Remove(Chip);

                Graph = new DependencyGraph(Chips);
            }
        }

        public void UpdateNext()
        {
            lock (Updates)
            {
                while (Updates.Size > 0)
                {
                    var chip = Updates.Pop();

                    if (!chip.IsPure || !Updates.Contains(chip)) // always tick non-pure chips, defer ticks for scheduled chips
                    {
                        chip.Tick();
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
    }
}