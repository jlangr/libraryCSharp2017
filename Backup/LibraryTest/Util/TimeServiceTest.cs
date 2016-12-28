using System;
using NUnit.Framework;
using Library.Util;
using NUnit.Framework.SyntaxHelpers;

namespace LibraryTest.Util
{
    [TestFixture]
    public class TimeServiceTest
    {
        [Test]
        public void ReturnsNowByDefault()
        {
            var span = DateTime.Now - TimeService.Now;
            Assert.IsTrue(span.Seconds < 1);
        }

        [Test]
        public void SupportsInjectingNextTime()
        {
            var now = DateTime.Now;
            TimeService.NextTime = now;
            Assert.That(TimeService.Now, Is.EqualTo(now));
        }

        [Test]
        public void InjectionIsOneTimeUseOnly()
        {
            var then = new DateTime(1999, 12, 31);
            TimeService.NextTime = then;
            Assert.That(TimeService.Now, Is.EqualTo(then));
            Assert.IsTrue((DateTime.Now - TimeService.Now).Seconds < 1);
        }
    }
}
