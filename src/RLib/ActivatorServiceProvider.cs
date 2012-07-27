using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLib
{
    public class ActivatorServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            return Activator.CreateInstance(serviceType);
        }
    }
}
