using CircuitSim2.Chips.Functors;
using CircuitSim2.IO;
using System;
using System.Collections.Generic;

namespace CircuitSim2.Chips.List
{
    [PureChip]
    public class Map<T, U> : ChipBase
    {
        [NonSerialized]
        private readonly Constant<T> elem;

        [NonSerialized]
        private UnaryFunctor<T, U> function;
        private Type function_type;
        [ChipProperty]
        public Type Function
        {
            get
            {
                return function_type;
            }
            set
            {
                function?.Dispose();

                function = Instantiate(value);

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private UnaryFunctor<T, U> Instantiate(Type value)
        {
            if (!value.IsSubclassOf(typeof(UnaryFunctor<T, U>)))
            {
                throw new ArgumentException("Invalid functor chip", nameof(Function));
            }

            var func = Activator.CreateInstance(value) as UnaryFunctor<T, U>;
            func.AutoTick = true;
            func.Inputs.A.Attach(elem.Outputs.Out);

            function_type = value;

            return func;
        }

        [NonSerialized]
        public readonly GenericInput<IEnumerable<T>> Inputs;
        [NonSerialized]
        public readonly GenericOutput<IEnumerable<U>> Outputs;

        public Map()
        {
            InputSet = (Inputs = new GenericInput<IEnumerable<T>>(this));
            OutputSet = (Outputs = new GenericOutput<IEnumerable<U>>(this));

            elem = new Functors.Constant<T>();
        }

        [NonSerialized]
        private List<U> _out;

        public sealed override void Compute()
        {
            if(function == null)
            {
                Function = function_type;
            }

            _out = new List<U>();

            foreach (var item in Inputs.A.Value)
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
        [NonSerialized]
        private readonly Constant<T> elem;
        
        [NonSerialized]
        private UnaryFunctor<T, bool> predicate;
        private Type predicate_type;
        [ChipProperty]
        public Type Predicate
        {
            get
            {
                return predicate_type;
            }
            set
            {
                predicate?.Dispose();

                predicate = Instantiate(value);

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private UnaryFunctor<T, bool> Instantiate(Type value)
        {
            if (!value.IsSubclassOf(typeof(UnaryFunctor<T, bool>)))
            {
                throw new ArgumentException("Invalid functor chip", nameof(Predicate));
            }

            var func = Activator.CreateInstance(value) as UnaryFunctor<T, bool>;
            func.AutoTick = true;
            func.Inputs.A.Attach(elem.Outputs.Out);

            predicate_type = value;

            return func;
        }

        [NonSerialized]
        public readonly GenericInput<IEnumerable<T>> Inputs;
        [NonSerialized]
        public readonly GenericOutput<IEnumerable<T>> Outputs;

        public Filter()
        {
            InputSet = (Inputs = new GenericInput<IEnumerable<T>>(this));
            OutputSet = (Outputs = new GenericOutput<IEnumerable<T>>(this));

            elem = new Functors.Constant<T>();
        }

        [NonSerialized]
        private List<T> _out;
        public sealed override void Compute()
        {
            if(predicate == null)
            {
                Predicate = predicate_type;
            }
            
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
        [NonSerialized]
        private readonly Constant<T> elem;
        [NonSerialized]
        private readonly Constant<U> acc;

        [NonSerialized]
        private BinaryFunctor<U, T, U> function;
        private Type function_type;
        [ChipProperty]
        public Type Function
        {
            get
            {
                return function_type;
            }
            set
            {
                function?.Dispose();

                function = Instantiate(value);

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private BinaryFunctor<U, T, U> Instantiate(Type value)
        {
            if (!value.IsSubclassOf(typeof(BinaryFunctor<U, T, U>)))
            {
                throw new ArgumentException("Invalid functor chip", nameof(Function));
            }

            var func = Activator.CreateInstance(value) as BinaryFunctor<U, T, U>;
            func.AutoTick = true;
            func.Inputs.A.Attach(acc.Outputs.Out);
            func.Inputs.B.Attach(elem.Outputs.Out);

            function_type = value;

            return func;
        }

        private U initialAcc;
        [ChipProperty]
        public U InitialAccumulator
        {
            get
            {
                return initialAcc;
            }
            set
            {
                initialAcc = value;

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        [NonSerialized]
        public readonly GenericInput<IEnumerable<T>> Inputs;
        [NonSerialized]
        public readonly GenericOutput<U> Outputs;

        public Fold()
        {
            InputSet = (Inputs = new GenericInput<IEnumerable<T>>(this));
            OutputSet = (Outputs = new GenericOutput<U>(this));

            elem = new Constant<T>();
            acc = new Constant<U>();
        }

        [NonSerialized]
        private U _out;

        public sealed override void Compute()
        {
            if(function == null)
            {
                Function = function_type;
            }

            acc.Value = InitialAccumulator;

            foreach (var item in Inputs.A.Value)
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
        protected override IEnumerable<T> NextValue()
        {
            return new List<T>();
        }
    }

    public class Generator<T> : Functors.Generator<IEnumerable<T>>
    {
        [NonSerialized]
        private readonly Functors.Constant<bool> clk;

        [NonSerialized]
        private Functors.Generator<T> function;
        private Type function_type;
        [ChipProperty]
        public Type Function
        {
            get
            {
                return function_type;
            }
            set
            {
                function?.Dispose();

                function = Instantiate(value);

                if (AutoTick)
                {
                    Tick();
                }
            }
        }

        private Functors.Generator<T> Instantiate(Type value)
        {
            if (!value.IsSubclassOf(typeof(Functors.Generator<T>)))
            {
                throw new ArgumentException("Invalid functor chip", nameof(Function));
            }

            var func = Activator.CreateInstance(value) as Functors.Generator<T>;
            func.AutoTick = true;
            func.Inputs.Clk.Attach(clk.Outputs.Out);

            function_type = value;

            return func;
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

        public Generator()
        {
            clk = new Constant<bool>();
        }

        protected override IEnumerable<T> NextValue()
        {
            if(function == null)
            {
                Function = function_type;
            }

            var list = new List<T>();
            for (int i = 0; i < Count; i++)
            {
                clk.Value = true;
                clk.Value = false;
                list.Add(function.Outputs.Out.Value);
            }
            return list;
        }
    }


}
