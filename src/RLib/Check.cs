using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLib
{
    public static class Check
    {
        public static void IsNull(object obj, string parameterName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}
