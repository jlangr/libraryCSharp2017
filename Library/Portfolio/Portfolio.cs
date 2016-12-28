using System.Collections.Generic;

namespace Library.Portfolio
{
    public delegate decimal PriceAlgorithm(string symbol);

    public class Portfolio
    {
        private readonly IDictionary<string, int> holdings = new Dictionary<string, int>();
        private readonly IStockLookupService service;
        private PriceAlgorithm calculatePrice;

        public Portfolio(IStockLookupService service)
        {
            this.service = service;
        }

        public Portfolio()
        {
            service = StockServiceFactory.Service;
        }

        public int HoldingCount
        {
            get { return holdings.Count; }
        }

        public bool IsEmpty
        {
            get { return 0 == HoldingCount; }
        }

        public void Purchase(string symbol, int shares)
        {
            if (symbol == "")
                throw new InvalidSymbolException();
            holdings[symbol] = SharesOf(symbol) + shares;
        }

        public int SharesOf(string symbol)
        {
            if (!holdings.ContainsKey(symbol))
                return 0;
            return holdings[symbol];
        }

        public PriceAlgorithm PriceAlgorithm
        {
            set
            {
                calculatePrice = value;
            }
        }

        public decimal Value()
        {
            var total = 0m;
            foreach (var entry in holdings)
            {
                var symbol = entry.Key;
                var shares = entry.Value;
                total += service.CurrentPrice(symbol) * shares;
            }
            return total;
        }

        public decimal CurrentValueUsingDelegate()
        {
            decimal total = 0m;
            foreach (KeyValuePair<string, int> entry in holdings)
            {
                string symbol = entry.Key;
                int shares = entry.Value;
                total += calculatePrice(symbol) * shares;
            }
            return total;
        }

        public decimal CurrentValue()
        {
            decimal total = 0m;
            foreach (var entry in holdings)
            {
                var symbol = entry.Key;
                var shares = entry.Value;
                total += service.CurrentPrice(symbol) * shares;
            }
            return total;
        }

        public decimal CurrentValueUsingFactory()
        {
            var total = 0m;
            foreach (var entry in holdings)
            {
                var symbol = entry.Key;
                var shares = entry.Value;
                total += shares * StockServiceFactory.Service.CurrentPrice(symbol);
            }
            return total;
        }

        public decimal CurrentValueUsingGetter()
        {
            var total = 0m;
            foreach (var entry in holdings)
            {
                var symbol = entry.Key;
                var shares = entry.Value;
                total += StockLookupService.CurrentPrice(symbol) * shares;
            }
            return total;
        }

        public virtual IStockLookupService StockLookupService
        {
            get { return new Nasdaq(); }
        }
    }
}
