using System.Collections.Generic;
using System.Text;

namespace RLib.Extensions
{
    public static class ByteExtensions
    {
        private const string HexFormatString = "x2";

        public static string ToHexString(this IEnumerable<byte> buffer)
        {
            var builder = new StringBuilder();
            foreach (var item in buffer)
            {
                builder.Append(item.ToString(HexFormatString));
            }

            return builder.ToString();
        }

        public static string ComputeMD5Hash(this IEnumerable<byte> buffer)
        {
            var builder = new StringBuilder();
            foreach (var item in buffer)
            {
                builder.Append(item.ToString(HexFormatString));
            }

            return builder.ToString();
        }
    }
}
