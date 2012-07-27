using System;
using RLib.Extensions;
using System.Security.Cryptography;
using System.Text;

namespace RLib.Security.Cryptography
{
	public static class Crypto
	{
		private const DataProtectionScope Scope = DataProtectionScope.CurrentUser;
		
		public const int DefaultSaltLength = 8;
		
		/// <summary>
		/// Encrypts the specified text.
		/// </summary>
		/// <param name='text'>Text to encrypt</param>
		/// <exception cref='ArgumentNullException'>
		/// Is thrown when an argument passed to a method is invalid because it is <see langword="null" /> .
		/// </exception>
		public static string Encrypt(string text)
		{
			if (text == null) throw new ArgumentNullException("text");
			
			var plainTextBytes = Encoding.Unicode.GetBytes(text);
			byte[] encryptedBytes = ProtectedData.Protect(plainTextBytes, null, Scope);
			
			return Convert.ToBase64String(encryptedBytes);
		}
		
		/// <summary>
		/// Decrypts the specified text.
		/// </summary>
		/// <param name='text'>Text to decrypt</param>
		/// <exception cref='ArgumentNullException'>
		/// Is thrown when an argument passed to a method is invalid because it is <see langword="null" /> .
		/// </exception>
		public static string Decrypt(string text)
		{
			if (text == null) throw new ArgumentNullException("text");
			
			var encryptedBytes = Convert.FromBase64String(text);
			var decryptedBytes = ProtectedData.Unprotect(encryptedBytes, null, Scope);
			
			return Encoding.Unicode.GetString(decryptedBytes);
		}
		
        /// <summary>
        /// Generates a salt used for encryption key derivation.
        /// </summary>
        /// <param name="length">The length, in bytes, of the salt to generate. The default length is <see cref="DefaultSaltLength"/>.</param>
        /// <returns>An array of bytes with a cryptographically strong random sequence of values.</returns>
        public static byte[] GenerateSalt(int length = DefaultSaltLength)
        {
            using (var rng = new RNGCryptoServiceProvider())
			{
	            var buffer = new byte[length];
	            rng.GetBytes(buffer);
	
	            return buffer;
			}
        }
	}
}

