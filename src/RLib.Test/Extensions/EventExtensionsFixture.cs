using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RLib.Extensions
{
    [TestFixture]
    public class EventExtensionsFixture
    {
        private class Account
        {
            public event EventHandler<EventArgs> PostTransaction;

            public decimal Balance { get; private set; }

            public void Deposit(decimal amount)
            {
                Balance += amount;
                PostTransaction.Raise(this, new EventArgs());
            }
        }

        [Test]
        public void Raise_Raises_Event_When_At_Least_One_Subscriber()
        {
            bool eventRaised = false;
            Account account = new Account();
            EventHandler<EventArgs> handler = (s, e) => eventRaised = true;
            account.PostTransaction += handler;

            account.Deposit(1000m);

            Assert.That(eventRaised, Is.True);

            account.PostTransaction -= handler;
        }
    }
}
