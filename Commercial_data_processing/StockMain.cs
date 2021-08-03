using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Commercial_data_processing
{
    public class StockMain
    {
        public void ReadJsonFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    StockAccount stac = new StockAccount();
                    string jsonData = File.ReadAllText(filePath);

                    StocksModel jsonObjectarray = JsonConvert.DeserializeObject<StocksModel>(jsonData);

                    Console.WriteLine("\n Displaying Available accounts . Use this to work with options.");
                    Console.WriteLine(" - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");
                    foreach (StocksModel.AccountClass ac in jsonObjectarray.AccountShares)
                    {
                        Console.Write(" {0}\t\t", ac.AccName);
                    }
                    Console.WriteLine("\n - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");

                    Console.Write("\n Select operation :  1.Select account for Buy and Selling shares \t 2.Add new account \n Type Option number: ");
                    int option = int.Parse(Console.ReadLine());

                    if (option == 1)
                    {
                        Console.Write(" Type account name : ");
                        string searchName = Console.ReadLine();
                        int present = 0;
                        foreach (StocksModel.AccountClass ac in jsonObjectarray.AccountShares)
                        {
                            if (ac.AccName.ToUpper() == searchName.ToUpper())
                            {
                                present = 1;
                                Console.Write(" | Account present..");
                            }
                        }
                        if (present == 1)
                        {
                            //go to operations
                            Console.WriteLine(" Using account of user : " + searchName);

                            Console.WriteLine("\n Displaying Available Stocks List. ");
                            DisplayStocks();

                            Console.Write(" Available Options :  1.Buy share\t2.Sell share \t|\t Your choice : ");
                            int ShareOption = int.Parse(Console.ReadLine());
                            
                            if (ShareOption == 1)
                            {
                                Console.Write(" Enter stock symbol : ");
                                string searchSymbol = Console.ReadLine();

                                Console.Write(" Enter Number of shares you want to buy of {0} : ", searchSymbol);
                                int userBuyNum = int.Parse(Console.ReadLine());

                                foreach (StocksModel.StockClass sc in jsonObjectarray.StocksList)
                                {
                                    if (sc.StockSymbol.ToUpper() == searchSymbol.ToUpper() && (sc.NumberOfShare>userBuyNum))
                                    {
                                        Console.WriteLine(" Shares availble.");
                                        Buy(searchSymbol, searchName, userBuyNum);
                                        File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonObjectarray));
                                        Console.WriteLine("\n Displaying Available Stocks List. ");
                                        DisplayStocks();
                                        Console.WriteLine("\n Displaying Available accounts List. ");
                                        DisplayAccounts();
                                        ReadJsonFile(filePath);
                                    }
                                    else
                                    {
                                        Console.WriteLine(" Order can not be proceesed.");
                                    }
                                }
                            }

                            else if (ShareOption == 2)
                            {
                                Console.WriteLine("\n Shares available with you.. ");
                                DisplayOneAccount(searchName);

                                Console.Write(" Enter stock symbol : ");
                                string searchSymbol = Console.ReadLine();

                                Console.Write(" Enter Number of shares you want to sell of {0} : ", searchSymbol);
                                int userSellNum = int.Parse(Console.ReadLine());
                                int shareOk = 0;
                                foreach (StocksModel.AccountClass ac in jsonObjectarray.AccountShares)
                                {
                                    if (ac.StockSymbol.ToUpper() == searchSymbol.ToUpper() && (ac.AccSharesNos >= userSellNum))
                                    {
                                        Console.WriteLine(" Shares availble.");
                                        shareOk = 1;
                                    }
                                }
                                if(shareOk==1)
                                {
                                    Sell(searchSymbol, searchName, userSellNum);
                                    File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonObjectarray));
                                    Console.WriteLine("\n Displaying Available Stocks List. ");
                                    DisplayStocks();
                                    Console.WriteLine("\n Displaying Available accounts List. ");
                                    DisplayAccounts();
                                    ReadJsonFile(filePath);
                                }
                                else
                                {
                                    Console.WriteLine(" Order can not be proceesed.");
                                }
                            }   //sell completed.

                            else
                            {
                                Console.WriteLine(" Invalid Option. Please retry..");
                                ReadJsonFile(filePath);
                            }
                        }
                        else
                        {
                            Console.WriteLine(" Name not present...");
                            ReadJsonFile(filePath);
                        }
                    }   //option1 - account exists completed


                    if (option == 2)
                    {
                        //create new account
                        jsonObjectarray.AccountShares = stac.AddToList(jsonObjectarray.AccountShares);
                        //add records to json file
                        File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonObjectarray));
                        Console.WriteLine("\n Displaying Available Stocks List. ");
                        DisplayStocks();
                        ReadJsonFile(filePath);
                    }


                    void DisplayStocks()
                    {
                        Console.WriteLine(" - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");

                        Console.WriteLine(" StockSymbol \t StockName \t\t SharePrice \t NumberOfShares ");
                        foreach (StocksModel.StockClass sto in jsonObjectarray.StocksList)
                        {
                            Console.WriteLine(" {0}\t\t{1}\t\t{2}\t\t{3}", sto.StockSymbol, sto.CompanyName, sto.SharePrice, sto.NumberOfShare);
                        }
                        Console.WriteLine("\n  - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");
                    }

                    void DisplayOneAccount(string accName)
                    {
                        Console.WriteLine("  - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");

                        Console.WriteLine(" AccountName \t StockName \t StockSymbol  NumberOfShares SharePrice  BuyDateTime \t\t Total");
                        foreach (StocksModel.AccountClass act in jsonObjectarray.AccountShares)
                        {
                            if (act.AccName.ToUpper() == accName.ToUpper())
                            {
                                Console.WriteLine(" {0}\t\t{1}\t\t{2}\t{3}\t\t{4}\t{5}\t{6}", act.AccName, act.StockName, act.StockSymbol, act.AccSharesNos, act.SharePrice, act.OperateDateTime, act.AccSharesNos * act.SharePrice);
                            }
                        }
                        Console.WriteLine("\n  - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");
                    }

                    void DisplayAccounts()
                    {
                        Console.WriteLine("  - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");

                        Console.WriteLine(" AccountName \t StockName \t StockSymbol  NumberOfShares SharePrice  BuyDateTime \t\t Total");
                        foreach (StocksModel.AccountClass act in jsonObjectarray.AccountShares)
                        {
                            Console.WriteLine(" {0}\t\t{1}\t\t{2}\t{3}\t\t{4}\t{5}\t{6}", act.AccName, act.StockName, act.StockSymbol, act.AccSharesNos, act.SharePrice, act.OperateDateTime, act.AccSharesNos*act.SharePrice);
                        }
                        Console.WriteLine("\n  - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");
                    }

                    void Buy(string stockSymbol, string userName, int userShares)
                    {

                        foreach (StocksModel.StockClass sc in jsonObjectarray.StocksList)
                        {
                            if (sc.StockSymbol.ToUpper() == stockSymbol.ToUpper())
                            {
                                //subtract share from stock
                                sc.NumberOfShare -= userShares;

                                foreach (StocksModel.AccountClass ac in jsonObjectarray.AccountShares)
                                {
                                    if (ac.AccName.ToUpper() == userName.ToUpper())
                                    {
                                        ac.SharePrice = sc.SharePrice;
                                        ac.StockSymbol = stockSymbol;
                                        ac.StockName = sc.CompanyName;
                                        //add share to useraccount
                                        ac.AccSharesNos = ac.AccSharesNos + userShares;
                                        ac.OperateDateTime = DateTime.Now;
                                        Console.WriteLine(" Buy share success.");
                                    }
                                }
                            }
                        }
                    }

                    void Sell(string stockSymbol, string userName, int userSellShares)
                    {

                        foreach (StocksModel.AccountClass ac in jsonObjectarray.AccountShares)
                        {
                            if (ac.AccName.ToUpper() == userName.ToUpper())
                            {
                                //subtract share from user
                                ac.AccSharesNos -= userSellShares;
                                foreach (StocksModel.StockClass sc in jsonObjectarray.StocksList)
                                {
                                    if (sc.StockSymbol.ToUpper() == stockSymbol.ToUpper())
                                    {
                                        //add share to stocks
                                        sc.NumberOfShare += userSellShares;
                                        ac.OperateDateTime = DateTime.Now;
                                        Console.WriteLine(" Share sell success.");
                                    }
                                }
                            }
                        }
                    }

                }
                else
                {
                    Console.WriteLine(" \n Specified filepath does not exist.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
