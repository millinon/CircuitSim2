using CircuitSim2.Chips.Functors;
using CircuitSim2.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitSim2.Chips.List
{
    [PureChip]
    public class Map<T, U> : ChipBase
    {
        private readonly Constant<T> elem;

        private UnaryFunctor<T, U> function;
        [ChipProperty]
        public Type Function
        {
            get
            {
                return function.GetType();
            }
            set
            {
                if (!value.IsSubclassOf(typeof(UnaryFunctor<T, U>)))
                {
                    throw new ArgumentException("Invalid functor chip", nameof(Function));
                }

                if(function != null)
                {
                    function.Detach();
                    function.Dispose();
                }

                function = Activator.CreateInstance(value, new object[] { null, null }) as UnaryFunctor<T, U>;
                function.AutoTick = true;
                function.Inputs.A.Attach(elem.Outputs.Out);

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        public readonly GenericInput<IEnumerable<T>> Inputs;
        public readonly GenericOutput<IEnumerable<U>> Outputs;

        public Map(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = (Inputs = new GenericInput<IEnumerable<T>>(this));
            OutputSet = (Outputs = new GenericOutput<IEnumerable<U>>(this));

            elem = new Functors.Constant<T>(null, null);
        }

        private List<U> _out;
        public sealed override void Compute()
        {
            _out = new List<U>();

            foreach(var item in Inputs.A.Value)
            {
                elem.Value = item;
                _out.Add(function.Outputs.Out.Value);
            }
        }

        public sealed override void Commit()
        {
            Outputs.Out.Value = _out;
        }

        public sealed override void Tick()
        {
            base.Tick();
        }
    }

    [PureChip]
    public class Filter<T> : ChipBase
    {
        private readonly Constant<T> elem;

        private UnaryFunctor<T, bool> predicate;
        [ChipProperty]
        public Type Predicate
        {
            get
            {
                return predicate.GetType();
            }
            set
            {
                if (!value.IsSubclassOf(typeof(UnaryFunctor<T, bool>)))
                {
                    throw new ArgumentException("Invalid functor chip", nameof(Predicate));
                }

                if (predicate != null)
                {
                    predicate.Detach();
                    predicate.Dispose();
                }

                predicate = Activator.CreateInstance(value, new object[] { null, null }) as UnaryFunctor<T, bool>;
                predicate.AutoTick = true;
                predicate.Inputs.A.Attach(elem.Outputs.Out);

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        public readonly GenericInput<IEnumerable<T>> Inputs;
        public readonly GenericOutput<IEnumerable<T>> Outputs;

        public Filter(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = (Inputs = new GenericInput<IEnumerable<T>>(this));
            OutputSet = (Outputs = new GenericOutput<IEnumerable<T>>(this));

            elem = new Functors.Constant<T>(null, null);
        }

        private List<T> _out;
        public sealed override void Compute()
        {
            _out = new List<T>();

            foreach (var item in Inputs.A.Value)
            {
                elem.Value = item;

                if (predicate.Outputs.Out.Value)
                {
                    _out.Add(item);
                }
            }
        }

        public sealed override void Commit()
        {
            Outputs.Out.Value = _out;
        }

        public sealed override void Tick()
        {
            base.Tick();
        }
    }

    [PureChip]
    public class Fold<T, U> : ChipBase
    {
        private readonly Constant<T> elem;
        private readonly Constant<U> acc;

        private BinaryFunctor<U,T,U> function;
        [ChipProperty]
        public Type Function
        {
            get
            {
                return function.GetType();
            }
            set
            {
                if (!value.IsSubclassOf(typeof(BinaryFunctor<U, T, U>)))
                {
                    throw new ArgumentException("Invalid functor chip", nameof(Function));
                }

                if (function != null)
                {
                    function.Detach();
                    function.Dispose();
                }

                function = Activator.CreateInstance(value, new object[] { null, null }) as BinaryFunctor<U, T, U>;
                function.AutoTick = true;
                function.Inputs.A.Attach(acc.Outputs.Out);
                function.Inputs.B.Attach(elem.Outputs.Out);

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private U initialAcc;
        [ChipProperty]
        public U InitialAccumulator
        {
            get
            {
                return initialAcc;
            } set
            {
                initialAcc = value;

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        public readonly GenericInput<IEnumerable<T>> Inputs;
        public readonly GenericOutput<U> Outputs;

        public Fold(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            InputSet = (Inputs = new GenericInput<IEnumerable<T>>(this));
            OutputSet = (Outputs = new GenericOutput<U>(this));

            elem = new Constant<T>(null, null);
            acc = new Constant<U>(null, null);
        }

        private U _out;

        public sealed override void Compute()
        {
            acc.Value = InitialAccumulator;

            foreach(var item in Inputs.A.Value)
            {
                elem.Value = item;

                acc.Value = function.Outputs.Out.Value;
            }

            _out = acc.Value;
        }

        public sealed override void Commit()
        {
            Outputs.Out.Value = _out;
        }

        public sealed override void Tick()
        {
            base.Tick();
        }
    }

    public class Empty<T> : Functors.Generator<IEnumerable<T>>
    {
        public Empty(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
        }

        protected override IEnumerable<T> NextValue()
        {
            return new List<T>();
        }
    }

    public class Generator<T> : Functors.Generator<IEnumerable<T>>
    {
        private readonly Functors.Constant<bool> clk;

        private Functors.Generator<T> function;
        [ChipProperty]
        public Type Function
        {
            get
            {
                return function.GetType();
            }
            set
            {
                if (!value.IsSubclassOf(typeof(Functors.Generator<T>)))
                {
                    throw new ArgumentException("Invalid functor chip", nameof(Function));
                }

                if (function != null)
                {
                    function.Detach();
                    function.Dispose();
                }

                function = Activator.CreateInstance(value, new object [] { null, null }) as Functors.Generator<T>;
                function.AutoTick = true;
                function.Inputs.Clk.Attach(clk.Outputs.Out);

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private int count;
        [ChipProperty]
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        public Generator(ChipBase ParentChip, Engine.Engine Engine) : base(ParentChip, Engine)
        {
            clk = new Constant<bool>(null, null);
        }

        protected override IEnumerable<T> NextValue()
        {
            var list = new List<T>();
            for(int i = 0; i < Count; i++)
            {
                clk.Value = true;
                clk.Value = false;
                list.Add(function.Outputs.Out.Value);
            }
            return list;
        }
    }


}
