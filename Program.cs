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
                if(Math.Abs(time.Minute - DateTime.Now.Minute) == 1)
                {
                    time = DateTime.Now;
                    RatesManager.CheckRates();
                }
               
            }
        }
    }
}
