using LinuxServer.DataManagers;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinuxServer
{
    class Program
    {
        static DateTime timeWeather = DateTime.Now, timeRates = DateTime.Now, timeStockePrice = DateTime.Now;

        
        private static string rates = "Empty data";
        private static string stock = "Empty data";
        private static string weather = "Empty data";
        
        
        static async Task Main(string[] args)
        {
            await Task.Run(() =>
            {
                SendToPipe("WeatherForecast.pipe");
            });
            await Task.Run(() =>
            {
                SendToPipe("ExchangeRate.pipe");
            });
            await Task.Run(() =>
            {
                SendToPipe("StockPrice.pipe");
            });
            while (true)
            {
                if(Math.Abs(timeRates.Day - DateTime.Now.Day) == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{DateTime.Now} Rate checker is working...");
                    timeRates = timeRates.AddDays(1);
                    rates = RatesManager.CheckRates();
                }
                if (Math.Abs(timeWeather.Hour - DateTime.Now.Hour) == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{DateTime.Now} Weather checker is working...");
                    timeWeather = timeWeather.AddHours(1);
                    weather = WeatherManager.CheckWeather();
                }
                if (Math.Abs(timeStockePrice.Minute - DateTime.Now.Minute) == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"{DateTime.Now} Stocke price checker is working...");
                    timeStockePrice = timeStockePrice.AddMinutes(1);
                    stock = StockPricesManager.CheckStockPrices();
                }
                
            }
        }
        static async Task SendToPipe(string pipeName)
        {
            while(true) {
                using (NamedPipeServerStream pipe =
                    new NamedPipeServerStream(pipeName, PipeDirection.Out, 1))
                {
                    await pipe.WaitForConnectionAsync();
                    Console.WriteLine("SENDING");
                    using (StreamWriter sw = new StreamWriter(pipe))
                    {
                        sw.AutoFlush = true;
                        string data = "";
                        if (pipeName.Contains("WeatherForecast")) data = weather; 
                        if (pipeName.Contains("ExchangeRate")) data = rates; 
                        if (pipeName.Contains("StockPrice")) data = stock; 
                        await sw.WriteAsync(data);
                    }
                }
            }
        } 
    }
}


