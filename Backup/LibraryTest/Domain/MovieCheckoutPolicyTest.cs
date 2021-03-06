using Library.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace LibraryTest.Domain
{
    [TestFixture]
    public class MovieCheckoutPolicyTest
    {
        private CheckoutPolicy policy;

        [SetUp]
        public void Initialize()
        {
            policy = new MovieCheckoutPolicy();
        }

        [Test]
        public void DailyAccumulatingFine()
        {
            var daysLate = 1;
            Assert.That(policy.FineAmount(daysLate++), Is.EqualTo(MovieCheckoutPolicy.PenaltyAmount + MovieCheckoutPolicy.DailyFineBasis * 1));
            Assert.That(policy.FineAmount(daysLate++), Is.EqualTo(MovieCheckoutPolicy.PenaltyAmount + MovieCheckoutPolicy.DailyFineBasis * 2));
            Assert.That(policy.FineAmount(daysLate), Is.EqualTo(MovieCheckoutPolicy.PenaltyAmount + MovieCheckoutPolicy.DailyFineBasis * 3));
        }
    }
}
