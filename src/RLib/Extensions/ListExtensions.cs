using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLib.Extensions
{
    public static class ListExtensions
    {
        public static IList<T> Clone<T>(this IList<T> list) where T : ICloneable
        {
            if (list == null)
            {
                throw new NullReferenceException("list");
            }

            var newList = new List<T>(list.Count);

            foreach (var item in list)
            {
                newList.Add((T)item.Clone());
            }

            return newList;
        }
    }
}
