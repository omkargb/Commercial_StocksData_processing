using System;
using System.Collections.Generic;
using System.Text;

namespace Commercial_data_processing
{
    public class StocksModel
    {
        public List<StockClass> StocksList { get; set; }
        public List<AccountClass> AccountShares { get; set; }

        public class StockClass
        {
            public string CompanyName { get; set; }
            public string StockSymbol { get; set; }
            public int NumberOfShare { get; set; }
            public int SharePrice { get; set; }
        }

        public class AccountClass
        {
            public string AccName { get; set; }
            public string StockName { get; set; }
            public string StockSymbol { get; set; }
            public int AccSharesNos { get; set; }
            public int SharePrice { get; set; }
            public DateTime? OperateDateTime { get; set; }
        }

    }
}
