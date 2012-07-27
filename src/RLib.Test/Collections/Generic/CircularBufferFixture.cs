using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RLib.Collections.Generic
{
    [TestFixture]
    public class CircularBufferFixture
    {
        [Test]
        public void Constructor_InvalidSize_ThrowsException([Values(0, -1)] int size)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CircularBuffer<int>(size));
        }

        [Test]
        public void Size_Get_ReturnsSizePassedToConstructor([Values(1, 10, 100)] int size)
        {
            var buffer = new CircularBuffer<int>(size);

            Assert.That(buffer.MaxSize, Is.EqualTo(size));
        }
    }
}
