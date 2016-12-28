using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Portfolio
{
    public class StubStockLookupService: IStockLookupService
    {
        public const string MSFT = "MSFT";
        public const string IBM = "IBM";
        public const decimal MSFT_VALUE = 16.83m;
        public const decimal IBM_VALUE = 88.62m;

        public decimal CurrentPrice(string symbol)
        {
            switch (symbol)
            {
                case MSFT: return MSFT_VALUE;
                case IBM: return IBM_VALUE;
            }
            return 0.0m;
        }
    }
}
