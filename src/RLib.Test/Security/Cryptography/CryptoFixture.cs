using System;
using NUnit.Framework;

namespace RLib.Security.Cryptography
{
	[TestFixture]
	public class CryptoFixture
	{
		[Test]
		public void Encrypt_ValidString_ReturnsEncryptedString()
		{
			string data = "This is sensitive data.";
			
			string encryptedData = Crypto.Encrypt(data);
			
			Assert.AreNotEqual(data, encryptedData);
		}
		
		[Test]
		public void Decrypt_EncryptedString_ReturnsOriginalString()
		{
			string data = "This is sensitive data.";
			
			string encryptedData = Crypto.Encrypt(data);
			string decryptedData = Crypto.Decrypt(encryptedData);
			
			Assert.AreEqual(data, decryptedData);
		}
		
		[Test]
		public void GenerateSalt_DefaultSaltLength_ReturnsArrayWithExpectedLength()
		{
			byte[] salt = Crypto.GenerateSalt();
			
			Assert.That(salt.Length == Crypto.DefaultSaltLength);
		}
		
		[Test]
		public void GenerateSalt_NonDefaultLength_ReturnsArrayWithExpectedLength()
		{
			byte[] salt = Crypto.GenerateSalt(Crypto.DefaultSaltLength + 8);
			
			Assert.That(salt.Length != Crypto.DefaultSaltLength);
		}
	}
}

