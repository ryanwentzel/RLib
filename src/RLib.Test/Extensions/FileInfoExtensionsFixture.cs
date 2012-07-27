using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using NUnit.Framework;

namespace RLib.Extensions
{
    [TestFixture]
    public class FileInfoExtensionsFixture : SelfCleaningFixture
    {
        [Test]
        public void ComputeMD5Hash_SameFile_ReturnsSameHash()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var file1 = new FileInfo(assembly.Location);
            var file2 = new FileInfo(assembly.Location);

            var hash1 = file1.ComputeMD5Hash();
            var hash2 = file2.ComputeMD5Hash();

            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void ComputeMD5Hash_DifferentFile_ReturnsDifferentHash()
        {
            var assembly1 = Assembly.GetExecutingAssembly();
            var assembly2 = Assembly.GetCallingAssembly();

            Assert.That(assembly1, Is.Not.EqualTo(assembly2));

            var file1 = new FileInfo(assembly1.Location);
            var file2 = new FileInfo(assembly2.Location);

            var hash1 = file1.ComputeMD5Hash();
            var hash2 = file2.ComputeMD5Hash();

            Assert.That(hash1, Is.Not.EqualTo(hash2));
        }

        [Test]
        public void ComputeMD5Hash_FileContentsChanged_ReturnsDifferentHash()
        {
            string fileName = Path.ChangeExtension(Guid.NewGuid().ToString("N"), "txt");
            FileInfo file = CreateFileWithRandomContents(fileName);
            var originalHash = file.ComputeMD5Hash();

            AppendRandomText(fileName);
            file.Refresh();

            var newHash = file.ComputeMD5Hash();

            Assert.That(newHash, Is.Not.EqualTo(originalHash));
        }

        [Test]
        public void ComputeNodeId_ValidFile_Returns40DigitHexValue()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var file = new FileInfo(assembly.Location);

            var nodeId = file.ComputeNodeId();

            Assert.That(nodeId.Length, Is.EqualTo(40));
        }

        [Test]
        public void GetShortenedNodeId_ValidFile_Returns12DigitHexValue()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var file = new FileInfo(assembly.Location);

            var nodeId = file.GetShortenedNodeId();

            Assert.That(nodeId.Length, Is.EqualTo(12));
        }

        [Test]
        public void GetShortenedNodeId_ValidFile_FullNodeIdStartsWithShortenedForm()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var file = new FileInfo(assembly.Location);

            var nodeId = file.ComputeNodeId();
            var shortenedNodeId = file.GetShortenedNodeId();

            Assert.That(nodeId.StartsWith(shortenedNodeId), Is.True);
        }

        private static FileInfo CreateFileWithRandomContents(string fileName, int sizeInBytes = 500)
        {
            using (var crypto = new RNGCryptoServiceProvider())
            using (var writer = File.CreateText(fileName))
            {
                writer.AutoFlush = true;

                byte[] buffer = new byte[sizeInBytes];
                crypto.GetNonZeroBytes(buffer);

                string fileContents = Convert.ToBase64String(buffer);
                writer.Write(fileContents);
            }

            return new FileInfo(fileName);
        }

        private static void AppendRandomText(string fileName)
        {
            using (var crypto = new RNGCryptoServiceProvider())
            {
                byte[] buffer = new byte[500];
                crypto.GetNonZeroBytes(buffer);

                string text = Convert.ToBase64String(buffer);
                File.AppendAllText(fileName, text);
            }
        }
    }
}
