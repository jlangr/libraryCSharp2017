using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Portfolio
{
    public interface IStockLookupService
    {
        decimal CurrentPrice(string symbol);
    }
}
