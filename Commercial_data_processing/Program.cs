using System;

namespace Commercial_data_processing
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\HP\source\repos\Commercial_data_processing\StocksFile.json";

            Console.WriteLine(" Welcome to Commercial_data_processing Stock_management Program");
            StockMain sm = new StockMain();
            sm.ReadJsonFile(filePath);
        }
    }
}
