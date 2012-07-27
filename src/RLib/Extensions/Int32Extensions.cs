using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLib.Extensions
{
    public static class Int32Extensions
    {
        public static bool IsEven(this int number)
        {
            return (number & 0x1) == 0;
        }

        public static bool IsOdd(this int number)
        {
            return (number & 0x1) == 1;
        }

        public static string ToString(this int? number, int defaultValue)
        {
            var num = number ?? defaultValue;
            return num.ToString();
        }

        public static DateTime January(this int day, int year)
        {
            return new DateTime(year, 1, day);
        }
    }
}
