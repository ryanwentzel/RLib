using System;
using System.Text;
using RLib.Security.Cryptography;

namespace RLib.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhitespace(this string str)
        {
            return (str == null || str.Trim() == string.Empty);
        }

        public static string Remove(this string str, params string[] targets)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (targets == null)
            {
                throw new ArgumentNullException("targets");
            }

            StringBuilder builder = new StringBuilder(str);

            foreach (string t in targets)
            {
                builder.Replace(t, string.Empty);
            }

            return builder.ToString();
        }

        public static string FormatWith(this string str, object arg0)
        {
            return string.Format(str, arg0);
        }

        public static string Reverse(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            var chars = str.ToCharArray();
            Array.Reverse(chars);

            return new string(chars);
        }

        public static byte[] ToByteArray(this string str)
        {
            if (str == null)
            {
                throw new NullReferenceException("str");
            }

            var bytes = Encoding.UTF8.GetBytes(str);

            return bytes;
        }

        public static byte[] ToByteArray(this string str, Encoding encoding)
        {
            str.ThrowIfNull("str");

            var bytes = encoding.GetBytes(str);
            return bytes;
        }
		
		public static string ToBase64String(this string str)
		{
			if (string.IsNullOrEmpty(str)) return str;
			
			var bytes = Encoding.UTF8.GetBytes(str);
			
			return Convert.ToBase64String(bytes);
		}
		
		public static string Encrypt(this string plainText)
		{
			return Crypto.Encrypt(plainText);
		}
		
		public static string Decrypt(this string cipher)
		{
			return Crypto.Decrypt(cipher);
		}

        public static bool EqualsOrdinalIgnoreCase(this string str, string value)
        {
            return str.Equals(value, StringComparison.OrdinalIgnoreCase);
        }
    }
}
