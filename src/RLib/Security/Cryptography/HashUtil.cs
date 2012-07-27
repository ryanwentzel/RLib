using System;
using System.Security.Cryptography;
using System.Text;

namespace RLib.Security.Cryptography
{
	public class HashUtil
	{
		private HashUtil()
		{
			// Private default constructor prevents instantiation.
		}
		
		public static string CreateMD5Hash(string input, Encoding encoding = null)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				throw new ArgumentException("input");
			}
			
			encoding = encoding ?? Encoding.ASCII;
			using(MD5 md5 = MD5.Create())
			{
				byte[] inputBytes = encoding.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(inputBytes);
				
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < hashBytes.Length; i++)
				{
					builder.Append(hashBytes[i].ToString("X2"));
				}
				
				return builder.ToString();
			}
		}
	}
}

