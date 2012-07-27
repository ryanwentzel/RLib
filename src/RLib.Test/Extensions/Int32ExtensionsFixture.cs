using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RLib.Extensions
{
    [TestFixture]
    public class Int32ExtensionsFixture
    {
        [Test]
        public void IsEven_Returns_True()
        {
            var evenNumbers = Enumerable.Range(2, 100).Where(x => x % 2 == 0);

            bool result = evenNumbers.All(x => x.IsEven());

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsEven_Returns_False()
        {
            var oddNumbers = Enumerable.Range(2, 100).Where(x => x % 2 != 0);

            bool result = oddNumbers.All(x => x.IsEven());

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsOdd_Returns_True()
        {
            var oddNumbers = Enumerable.Range(2, 100).Where(x => x % 2 != 0);

            bool result = oddNumbers.All(x => x.IsOdd());

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsOdd_Returns_False()
        {
            var evenNumbers = Enumerable.Range(2, 100).Where(x => x % 2 == 0);

            bool result = evenNumbers.All(x => x.IsOdd());

            Assert.That(result, Is.False);
        }

        [Test]
        public void ToString_Returns_Default_Value_As_String()
        {
            int? number = null;

            string result = number.ToString(3);

            Assert.That(result, Is.EqualTo(3.ToString()));
        }

        [Test]
        public void ToString_Returns_Number_As_String()
        {
            int? number = 5;

            string result = number.ToString(1);

            Assert.That(result, Is.EqualTo(5.ToString()));
        }

        [Test]
        public void January_Returns_Expected_Date()
        {
            DateTime date = 1.January(2011);

            Assert.That(date, Is.EqualTo(new DateTime(2011, 1, 1)));
        }
    }
}
