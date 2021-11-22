using Server.DataManagers;
using System;
using System.IO.MemoryMappedFiles;

namespace Server
{
    class Program
    {
        static DateTime timeWeather = DateTime.Now, timeRates = DateTime.Now, timeStockePrice = DateTime.Now;
        static void Main(string[] args)
        {
            MemoryMappedFile memoryMappedFileWeatherForecast = MemoryMappedFile.CreateNew("MMF_WeatherForecast", 10000);
            MemoryMappedFile memoryMappedFileExchangeRate = MemoryMappedFile.CreateNew("MMF_ExchangeRate", 10000);
            MemoryMappedFile memoryMappedFileStockPrices = MemoryMappedFile.CreateNew("MMF_StockPrices", 20000);
            while (true)
            {
                if(Math.Abs(timeRates.Day - DateTime.Now.Day) == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{DateTime.Now} Rate checker is working...");
                    timeRates = timeRates.AddDays(1);
                    RatesManager.CheckRates(memoryMappedFileExchangeRate);
                }
                if (Math.Abs(timeWeather.Hour - DateTime.Now.Hour) == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{DateTime.Now} Weather checker is working...");
                    timeWeather = timeWeather.AddHours(1);
                    WeatherManager.CheckWeather(memoryMappedFileWeatherForecast);
                }
                if (Math.Abs(timeStockePrice.Minute - DateTime.Now.Minute) == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"{DateTime.Now} Stocke price checker is working...");
                    timeStockePrice = timeStockePrice.AddMinutes(1);
                    StockPricesManager.CheckStockPrices(memoryMappedFileStockPrices);
                }
            }
        }
    }
}
