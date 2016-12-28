using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Portfolio
{
    public class StockServiceFactory
    {
        private static readonly IStockLookupService Default = new Nasdaq();
        private static IStockLookupService current = Default;
        public static IStockLookupService Service
        { 
            get { return current; }
            set { current = value; }
        }
        public static void Reset()
        {
            current = Default;
        }
    }
}
