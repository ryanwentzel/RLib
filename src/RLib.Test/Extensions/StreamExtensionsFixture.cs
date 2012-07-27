using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace RLib.Extensions
{
    [TestFixture]
    public class StreamExtensionsFixture
    {
        [Test]
        public void ReadFully_Reads_Complete_Stream()
        {
            MemoryStream stream = new MemoryStream();

            for (int i = 0; i < 100; i++)
            {
                byte[] bytes = BitConverter.GetBytes(i);
                stream.Write(bytes, 0, bytes.Length);
            }

            stream.Position = 0;
            int length = (int)stream.Length;
            byte[] data = stream.ReadFully(length);

            Assert.That(data.Length, Is.EqualTo(length));
        }
    }
}
