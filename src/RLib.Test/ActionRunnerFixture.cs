using System;

using NUnit.Framework;

namespace RLib
{
    [TestFixture]
    public class ActionRunnerFixture
    {
        [Test]
        public void RunSafely_NullAction_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => ActionRunner.RunSafely(null));
        }

        [Test]
        public void RunSafely_ActionThatThrows_DoesNotThrow()
        {
            Action action = () =>
            { throw new ApplicationException("testing"); };

            Assert.DoesNotThrow(() => ActionRunner.RunSafely(action));
        }
    }
}
