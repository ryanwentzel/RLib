using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLib.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null)
            {
                throw new NullReferenceException("source");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            foreach (var item in source)
            {
                action(item);
            }
        }

        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source != null)
            {
                using (var enumerator = source.GetEnumerator())
                {
                    return !enumerator.MoveNext();
                }
            }

            return true;
        }

        //public static IEnumerable<TSource> Apply<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        //{
        //    if (source == null)
        //    {
        //        throw new ArgumentNullException("source");
        //    }

        //    if (action == null)
        //    {
        //        throw new ArgumentNullException("action");
        //    }

        //    foreach (var item in source)
        //    {
        //        action(item);
        //        yield return item;
        //    }
        //}
    }
}
