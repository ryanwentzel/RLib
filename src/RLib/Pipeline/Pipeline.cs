using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLib.Pipeline
{
    public class Pipeline<T>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly List<IFilter<T>> filters;
        private int current;
        private Func<Action<T>> getNext;

        public Pipeline()
            : this(new ActivatorServiceProvider())
        {
        }

        public Pipeline(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            filters = new List<IFilter<T>>();
        }

        public int Count
        {
            get { return filters.Count; }
        }

        public Pipeline<T> Add(IFilter<T> filter)
        {
            filters.Add(filter);
            return this;
        }

        public Pipeline<T> Add(Type filterType)
        {
            Add((IFilter<T>)serviceProvider.GetService(filterType));
            return this;
        }

        public Pipeline<T> Add<TFilter>() where TFilter : IFilter<T>
        {
            Add(typeof(TFilter));
            return this;
        }

        public void Execute(T input)
        {
            getNext = () => current < filters.Count
                ? x => filters[current++].Execute(x, getNext())
                : new Action<T>(c => { });

            getNext().Invoke(input);
        }
    }
}
