using Library.Portfolio;
using NUnit.Framework;
//using Rhino.Mocks;

namespace LibraryTest.Portfolio
{
    [TestFixture]
    public class PortfolioTest
    {
        private Library.Portfolio.Portfolio portfolio;

        [SetUp]
        public void Initialize()
        {
            portfolio = new Library.Portfolio.Portfolio(new StubStockLookupService());
        }
        
        [Test]
        public void IsEmptyOnCreation()
        {
            AssertSize(0);
        }

        [Test]
        public void IsWorthlessOnCreation()
        {
            Assert.That(portfolio.CurrentValueUsingGetter(), Is.EqualTo(0));
        }

        [Test]
        public void CalculatesValueForSinglePurchaseUsingRhinoMocks()
        {
            //const string msft = "MSFT";
            //const decimal msftValue = 100m;

            //var service =
            //    MockRepository.GenerateMock<IStockLookupService>();
            //service.Stub(x => x.CurrentPrice(msft)).Return(msftValue);

            //var rhinoPortfolio = new Library.Portfolio.Portfolio(service); 
            //rhinoPortfolio.Purchase(msft, 5);
            //Assert.That(rhinoPortfolio.CurrentValue(), Is.EqualTo(msftValue * 5));
        }

        [Test]
        public void CalculatesValueForSinglePurchaseUsingDelegates()
        {
            //const string msft = "MSFT";
            //const decimal msftValue = 28.44m;
            //portfolio.PriceAlgorithm = delegate(string symbol)
            //                               {
            //                                   return symbol == msft ? msftValue : 0;
            //                               };
            //portfolio.Purchase(msft, 5);
            //Assert.That(portfolio.CurrentValueUsingDelegate(), Is.EqualTo(msftValue * 5));
        }

        [Test]
        public void SinglePurchase()
        {
            portfolio.Purchase(StubStockLookupService.MSFT, 5);
            AssertSize(1);
            Assert.That(portfolio.SharesOf(StubStockLookupService.MSFT), Is.EqualTo(5));
        }

        [Test]
        public void ReturnsValueForSingleHolding()
        {
            var handMockPortfolio = new Library.Portfolio.Portfolio(new StubStockLookupService());
            handMockPortfolio.Purchase(StubStockLookupService.MSFT, 3);
            Assert.That(handMockPortfolio.CurrentValue(), Is.EqualTo(3 * StubStockLookupService.MSFT_VALUE));
        }

        [Test]
        public void MultiplePurchasesSameSymbol()
        {
            portfolio.Purchase(StubStockLookupService.MSFT, 5);
            portfolio.Purchase(StubStockLookupService.MSFT, 10);
            AssertSize(1);
            Assert.That(portfolio.SharesOf(StubStockLookupService.MSFT), Is.EqualTo(10 + 5));
        }

        [Test]
        public void SinglePurchaseValueUsingFactory()
        {
            StockServiceFactory.Service = new StubStockLookupService();
            try
            {
                var factoryPortfolio = new Library.Portfolio.Portfolio();
                factoryPortfolio.Purchase(StubStockLookupService.MSFT, 3);
                Assert.That(factoryPortfolio.CurrentValue(), Is.EqualTo(3 * StubStockLookupService.MSFT_VALUE));
            }
            finally
            {
                StockServiceFactory.Reset();
            }
        }
  
        public class PortfolioWithOverrideStub : Library.Portfolio.Portfolio
        {
            public override IStockLookupService StockLookupService
            {
                get { return new StubStockLookupService(); }
            }
        }

        [Test]
        public void SinglePurchaseValueUsingOverride()
        {
            var overridePortfolio = new PortfolioWithOverrideStub();
            overridePortfolio.Purchase(StubStockLookupService.MSFT, 3);
            Assert.That(overridePortfolio.CurrentValueUsingGetter(), Is.EqualTo(3 * StubStockLookupService.MSFT_VALUE));
        }

        [Test]
        public void MultiplePurchasesDifferentSymbols()
        {
            portfolio.Purchase(StubStockLookupService.MSFT, 5);
            portfolio.Purchase(StubStockLookupService.IBM, 10);
            AssertSize(2);
            Assert.That(portfolio.SharesOf(StubStockLookupService.IBM), Is.EqualTo(10));
        }

        [Test]
        public void SumsValuesForMultipleHoldings()
        {

            portfolio.Purchase(StubStockLookupService.MSFT, 5);
            portfolio.Purchase(StubStockLookupService.IBM, 10);
            const decimal expectedValue = 5 * StubStockLookupService.MSFT_VALUE +
                                          10 * StubStockLookupService.IBM_VALUE;
            Assert.That(portfolio.CurrentValue(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ReturnsZeroSharesForUnpurchasedSymbol()
        {
            Assert.That(portfolio.SharesOf(StubStockLookupService.MSFT), Is.EqualTo(0));
        }

        //[Test]
        ////[ExpectedException(typeof(InvalidSymbolException))]
        //public void ThrowsOnPurchaseOfEmptySymbol()
        //{
        //    portfolio.Purchase("", 5);
        //}

        private void AssertSize(int expected)
        {
            Assert.That(portfolio.HoldingCount, Is.EqualTo(expected));
            Assert.That(portfolio.IsEmpty, Is.EqualTo(0 == expected));
        }
    }
}
