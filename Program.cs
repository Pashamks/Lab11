using Server.DataManagers;
using System;
using System.Timers;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime timeWeather = DateTime.Now, timeRates = DateTime.Now;
           
            while (true)
            {
                if(Math.Abs(timeRates.Day - DateTime.Now.Day) == 0)
                {
                    Console.WriteLine($"{DateTime.Now} Rate checker is working...");
                    timeRates = timeRates.AddDays(1);
                    RatesManager.CheckRates();
                }
                if (Math.Abs(timeWeather.Minute - DateTime.Now.Minute) == 1)
                {
                    Console.WriteLine($"{DateTime.Now} Weather checker is working...");
                    timeWeather = DateTime.Now;
                    WeatherManager.CheckWeather();
                }
            }
        }
    }
}
