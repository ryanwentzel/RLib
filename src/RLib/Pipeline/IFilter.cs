using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLib.Pipeline
{
    public interface IFilter<T>
    {
        void Execute(T context, Action<T> executeNext);
    }
}
