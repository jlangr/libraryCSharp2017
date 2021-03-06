using System;
using Library.Domain;
using NUnit.Framework;

namespace LibraryTest.Domain
{
    [TestFixture]
    public class HoldingTest
    {
        const int PatronId = 101;

        [Test]
        public void CreateWithClassificationAndCopyNumber()
        {
            var holding = new Holding("QA123", 2);
            Assert.That(holding.Barcode, Is.EqualTo("QA123:2"));
            Assert.That(holding.BranchId, Is.EqualTo(Holding.CheckedOutBranchId));
        }

        [Test]
        public void CreateSpecifingAlsoBranch()
        {
            const int branchId = 10;
            var holding = new Holding("QA123", 2, branchId);
            Assert.That(holding.Barcode, Is.EqualTo("QA123:2"));
            Assert.That(holding.BranchId, Is.EqualTo(branchId));
        }

        [Test]
        public void GenerateBarcode()
        {
            Assert.That(Holding.GenerateBarcode("QA234", 3), Is.EqualTo("QA234:3"));
        }

        [Test]
        public void ClassificationFromBarcode()
        {
            Assert.That(Holding.ClassificationFromBarcode("QA234:3"), Is.EqualTo("QA234"));
        }

        [Test]
        public void Co()
        {
            var holding = new Holding("X", 1);
            Assert.IsFalse(holding.IsCheckedOut);
            var now = DateTime.Now;

            var policy = CheckoutPolicies.BookCheckoutPolicy;
            holding.CheckOut(now, PatronId, policy);

            Assert.IsTrue(holding.IsCheckedOut);

            Assert.AreSame(policy, holding.CheckoutPolicy);
            Assert.That(holding.HeldByPatronId, Is.EqualTo(PatronId));

            var dueDate = now.AddDays(policy.MaximumCheckoutDays());
            Assert.That(holding.DueDate, Is.EqualTo(dueDate));
        }

        [Test]
        public void CheckIn()
        {
            var holding = new Holding("X", 1) {BranchId = 1};
            holding.CheckOut(DateTime.Now, PatronId, CheckoutPolicies.BookCheckoutPolicy);
            var tomorrow = DateTime.Now.AddDays(1);
            const int newBranchId = 2;
            
            holding.CheckIn(tomorrow, newBranchId);
            
            Assert.IsFalse(holding.IsCheckedOut);
            Assert.That(holding.HeldByPatronId, Is.EqualTo(Holding.NoPatron));
            Assert.That(holding.CheckOutTimestamp, Is.EqualTo(DateTime.MinValue));
            Assert.That(holding.BranchId, Is.EqualTo(newBranchId));
            Assert.That(holding.LastCheckedIn, Is.EqualTo(tomorrow));
        }
    }
}
