using System;
using System.Collections.Generic;
using System.Text;

namespace Commercial_data_processing
{
    class StockAccount
    {
        public List<StocksModel.AccountClass> AddToList(List<StocksModel.AccountClass> userlist)
        {
            StocksModel.AccountClass ac = new StocksModel.AccountClass();

            Console.Write(" Enter Name : ");
            ac.AccName = Console.ReadLine();
            string searchTerm = ac.AccName;
            int newAdd = 1;
            foreach (StocksModel.AccountClass sa in userlist)
            {
                if ((sa.AccName).ToUpper().Equals(searchTerm.ToUpper()))
                {
                    Console.WriteLine(" New Name already present.");
                    newAdd = 0;
                    break;
                }
            }
            if (newAdd == 1)
            {
                ac.StockName = "";
                ac.StockSymbol = "";
                ac.AccSharesNos = 0;
                ac.SharePrice = 0;
                ac.OperateDateTime = null;
                userlist.Add(ac);
                Console.WriteLine(" Account created. ");
            }
            return userlist;
        }
    }
}
