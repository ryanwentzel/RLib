using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RLib
{
    [TestFixture]
    [Ignore]
    public class EncryptFixture
    {
        [Test]
        public void Encrypt_Returns_Byte_Array()
        {
            string str = "This is a test string.";

            byte[] result = Encryption.Encrypt(str);

            CollectionAssert.IsNotEmpty(result);
        }

        [Test]
        public void Decrypt_Decrypts_String()
        {
            string str = "This is a test string.";

            byte[] encrypted = Encryption.Encrypt(str);
            byte[] result = Encryption.Decrypt(encrypted);

            CollectionAssert.IsNotEmpty(result);
        }
    }
}
