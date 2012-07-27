using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.IO.Compression;

namespace RLib
{
    public static class Encryption
    {
        private static byte[] _key = { 145, 12, 32, 245, 98, 132, 98, 214, 6, 77, 131, 44, 221, 3, 9, 50 };
        private static byte[] _iv = { 15, 122, 132, 5, 93, 198, 44, 31, 9, 39, 241, 49, 250, 188, 80, 7 };

        private const PaddingMode DefaultPadding = PaddingMode.ISO10126;

        public static byte[] Encrypt(string data)
        {
            using (Rijndael algorithm = Rijndael.Create())
            {
                algorithm.Key = _key;
                algorithm.IV = _iv;
                algorithm.Padding = DefaultPadding;

                using (ICryptoTransform encryptor = algorithm.CreateEncryptor())
                using (MemoryStream ms = new MemoryStream())
                using (Stream c = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (Stream d = new DeflateStream(c, CompressionMode.Compress))
                using (StreamWriter writer = new StreamWriter(d))
                {
                    writer.WriteLine(data);
                    writer.Flush();

                    return ms.ToArray();
                }
            }
        }

        public static byte[] Decrypt(byte[] encryptedData)
        {
            using (Rijndael algorithm = Rijndael.Create())
            {
                algorithm.Key = _key;
                algorithm.IV = _iv;
                algorithm.Padding = DefaultPadding;

                using (ICryptoTransform decryptor = algorithm.CreateDecryptor())
                using (MemoryStream ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                {
                    cs.Write(encryptedData, 0, encryptedData.Length);
                    cs.FlushFinalBlock();
                    using (Stream d = new DeflateStream(cs, CompressionMode.Decompress))
                    {
                        return ms.ToArray();
                    }
                }
            }
        }
    }
}
