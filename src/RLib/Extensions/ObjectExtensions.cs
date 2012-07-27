using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLib.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsIn<T>(this T obj, params T[] list)
        {
            if (obj == null)
            {
                throw new NullReferenceException("obj");
            }

            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            return list.Contains(obj);
        }

        public static bool Is<T>(this object obj) where T : class
        {
            if (obj == null)
            {
                throw new NullReferenceException("obj");
            }

            return obj is T;
        }

        public static bool IsNot<T>(this object obj) where T : class
        {
            if (obj == null)
            {
                throw new NullReferenceException("obj");
            }

            return !(obj is T);
        }

        public static T As<T>(this object obj) where T : class
        {
            if (obj == null)
            {
                throw new NullReferenceException("obj");
            }

            return obj as T;
        }

        public static T To<T>(this object obj) where T : class
        {
            if (obj == null)
            {
                throw new NullReferenceException("obj");
            }

            return (T)obj;
        }

        public static void ThrowIfNull<T>(this T obj, string referenceName) where T : class
        {
            if (obj == null)
            {
                throw new NullReferenceException(referenceName);
            }
        }
    }
}
