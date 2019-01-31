using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;

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
                    map = new Dictionary<CircuitSim2.Chips.ChipBase, Node>();

                    foreach(var chip in Chips)
                    {
                        map[chip] = new Node(chip);
                    }

                    foreach(var chip in Chips)
                    {
                        var node = map[chip];

                        node.Parents = node.Chip.InputSet.AllInputs.Where(input => input.IsAttached).Select(input => map[input.SourceBase.Chip]);

                        HashSet<Chips.ChipBase> sinks = new HashSet<Chips.ChipBase>();

                    Action<IO.InputBase> add = null;
                    add = input =>
                        {
                            foreach(var hook in input.Hooks)
                            {
                                add(hook);
                            }

                            sinks.Add(input.Chip);
                        };  

                    foreach (var output in chip.OutputSet.AllOutputs)
                        {


                            foreach(var sink in output.Sinks())
                            {
                            add(sink);
                            }
                        }

                        node.Children = sinks.Select(sink => map[sink]);
                    }

                    if (Chips.Count() > 0)
                    {
                        if(!Roots.Any())
                        {
                            throw new Exception("Cyclical circuit detected");
                        }
                    }
                }
            }
            
            private readonly HashSet<Chips.ChipBase> Chips;

            private DependencyGraph Graph;

            public class UpdateQueue
            {
                private readonly ConcurrentQueue<Chips.ChipBase> Queue;

                public UpdateQueue()
                {
                    Queue = new ConcurrentQueue<CircuitSim2.Chips.ChipBase>();
                }

                public void Push(Chips.ChipBase Chip) => Queue.Enqueue(Chip);

                public void Pop()
                {
                    if (Queue.TryDequeue(out Chips.ChipBase chip))
                    {
                        chip.Tick();
                    }
                }

                public int Size
                {
                    get => Queue.Count();
                }
            }

            private UpdateQueue Updates;

            public Engine()
            {
                Chips = new HashSet<Chips.ChipBase>();
                Updates = new UpdateQueue();
                
            }

            public void RegenerateGraph()
            {
                Graph = new DependencyGraph(Chips);
            }

            public void Register(Chips.ChipBase Chip)
            {
                Chips.Add(Chip);

                RegenerateGraph();
            }

            public void Unregister(Chips.ChipBase Chip)
            {
                if (Chips.Contains(Chip)) Chips.Remove(Chip);

                RegenerateGraph();
            }

            public void UpdateNext()
            {
                Updates.Pop();
            }

            public void ScheduleUpdate(Chips.ChipBase Chip)
            {
                Updates.Push(Chip);
            }
        }
    }