using Server.DataManagers;
using System;
using System.Timers;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var time = DateTime.Now;
           
            while (true)
            {
                if(Math.Abs(time.Day - DateTime.Now.Day) == 0)
                {
                    Console.WriteLine($"{DateTime.Now} Rate checker is working...");
                    time = time.AddDays(1);
                    RatesManager.CheckRates();
                }
               
            }
        }
    }
}
