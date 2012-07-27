using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RLib.Extensions
{
    [TestFixture]
    public class EnumerableExtensionsFixture
    {
        [Test]
        public void ForEach_Throws_If_Action_Is_Null()
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 1000);
            Action<int> action = null;

            Assert.Throws<ArgumentNullException>(() => numbers.ForEach(action));
        }

        [Test]
        public void ForEach_Performs_Action_On_Each_Item_In_Collection()
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 1000);
            int expectedSum = numbers.Sum();

            int sum = 0;
            numbers.ForEach(num => sum += num);

            Assert.That(sum, Is.EqualTo(expectedSum));
        }

        //[Test]
        //public void Apply_Throws_If_Action_Is_Null()
        //{
        //    IEnumerable<int> numbers = Enumerable.Range(1, 1000);
        //    Action<int> action = null;

        //    Assert.Throws<ArgumentNullException>(() => numbers.Apply(action));
        //}

        //[Test]
        //public void Apply_Performs_Action_On_Each_Item_In_Collection()
        //{
        //    IEnumerable<int> numbers = Enumerable.Range(1, 1000);
        //    List<int> result = new List<int>();
        //    Action<int> action = num => result.Add(num * 2);

        //    numbers.Apply(action);

        //    Assert.That(result.Count, Is.EqualTo(numbers.Count()));
        //    for (int i = 0; i < result.Count; i++)
        //    {
        //        int num = numbers.Skip(i + 1).First();
        //        Assert.That(result[i], Is.EqualTo(num * 2));
        //    }
        //}
    }
}
