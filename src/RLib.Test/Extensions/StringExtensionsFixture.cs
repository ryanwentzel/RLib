using NUnit.Framework;

namespace RLib.Extensions
{
    [TestFixture]
    public class StringExtensionsFixture
    {
        [Test]
        public void IsNullOrWhitespace_Returns_True_For_Null_String()
        {
            string str = null;
            bool result = str.IsNullOrWhitespace();

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsNullOrWhitespace_Returns_True_For_Empty_String()
        {
            string str = string.Empty;
            bool result = str.IsNullOrWhitespace();

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsNullOrWhitespace_Returns_True_For_Whitespace()
        {
            string str = "     ";
            bool result = str.IsNullOrWhitespace();

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsNullOrWhitespace_Returns_False()
        {
            string str = "abcdefg";
            bool result = str.IsNullOrWhitespace();

            Assert.That(result, Is.False);
        }

        [Test]
        public void Replace_Removes_Targets()
        {
            string str = "abcefghijklmnopabc12dfe89";

            string result = str.Remove("abc");

            Assert.That(result.Contains("abc"), Is.False);
        }

        [Test]
        public void FormatWith_Returns_Expected_String()
        {
            string format = "Hello {0}";
            string name = "DTWC";

            string result = format.FormatWith(name);

            Assert.That(result, Is.EqualTo(string.Format(format, name)));
        }

        [Test]
        public void Reverse_Reverses_String()
        {
            string str = "123456789";
            string rev = "987654321";

            string result = str.Reverse();

            Assert.That(result, Is.EqualTo(rev));
        }

        [Test]
        public void EqualsOrdinalIgnoreCase_Lowercase_Returns_True()
        {
            string str = "the quick brown fox";
            string value = "the quick brown fox";

            bool equals = str.EqualsOrdinalIgnoreCase(value);

            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsOrdinalIgnoreCase_Uppercase_Returns_True()
        {
            string str = "THE QUICK BROWN FOX";
            string value = "THE QUICK BROWN FOX";

            bool equals = str.Equals(value);

            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsOrdinalIgnoreCase_MixedCase_Returns_True()
        {
            string str = "the QUICK bRown foX";
            string value = "THE quick BROWN fox";

            bool equals = str.EqualsOrdinalIgnoreCase(value);

            Assert.That(equals, Is.True);
        }
    }
}
