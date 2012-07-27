using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RLib.Extensions
{
    [TestFixture]
    public class ObjectExtensions
    {
        [Test]
        public void IsIn_Returns_True()
        {
            int number = 10;
            int[] list = { 1, 98, 531, 10, 99, 10568, 42 };

            bool result = number.IsIn(list);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsIn_Returns_True_02()
        {
            int number = 10;

            bool result = number.IsIn(10);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsIn_Returns_False()
        {
            int number = 10;

            bool result = number.IsIn(new int[] { 1, 2, 3, 4, 5 });

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsIn_Returns_False_02()
        {
            int number = 10;

            bool result = number.IsIn(5);

            Assert.That(result, Is.False);
        }

        [Test]
        public void Is_Returns_True()
        {
            object obj = new List<int>();

            bool result = obj.Is<List<int>>();

            Assert.That(result, Is.True);
        }

        [Test]
        public void Is_Returns_True_02()
        {
            object obj = new List<string>();

            bool result = obj.Is<IEnumerable<string>>();

            Assert.That(result, Is.True);
        }

        [Test]
        public void Is_Returns_False()
        {
            object obj = new List<Exception>();

            bool result = obj.Is<Exception>();

            Assert.That(result, Is.False);
        }
    }
}
