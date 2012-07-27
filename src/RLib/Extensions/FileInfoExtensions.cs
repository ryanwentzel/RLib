using System.IO;
using System.Security.Cryptography;

namespace RLib.Extensions
{
    public static class FileInfoExtensions
    {
        public static string ComputeMD5Hash(this FileInfo file)
        {
            return ComputeHash(file, "MD5");
        }

        /// <summary>
        /// Computes the nodeid of a file.
        /// </summary>
        /// <param name="file">The file to compute a nodeid for.</param>
        /// <returns>A 40 digit hex value representing the file's nodeid.</returns>
        /// <remarks>
        /// The nodeid of a file is computed using the SHA1 hash function which generates 
        /// 160 bits (40 hex digits).
        /// </remarks>
        public static string ComputeNodeId(this FileInfo file)
        {
            return ComputeHash(file, "SHA1");
        }

        /// <summary>
        /// Returns the first 12 characters of a file's nodeid.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>A 12 digit hex string representing the shortened nodeid.</returns>
        public static string GetShortenedNodeId(this FileInfo file)
        {
            return ComputeNodeId(file).Substring(0, 12);
        }

        private static string ComputeHash(FileInfo file, string hashName)
        {
            byte[] buffer = null;
            using (var algorithm = HashAlgorithm.Create(hashName))
            using (var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
            {
                buffer = algorithm.ComputeHash(stream);
            }

            return buffer.ToHexString();
        }
    }
}
