using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Net;
using System.Text;


namespace Server.DataManagers
{
    public class StockPricesManager
    {
        const int divider = 6;
        public static void CheckStockPrices(MemoryMappedFile memoryMappedFile)
        {
            using (WebClient wc = new WebClient())
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://" + $@"www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={"IBM"}&apikey={"demo"}&datatype=csv");
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string respons;
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    respons = streamReader.ReadToEnd().Replace(",", "   ");
                }
                byte[] data = Encoding.UTF8.GetBytes(respons);
                using (MemoryMappedViewAccessor accessor = memoryMappedFile.CreateViewAccessor())
                {
                    accessor.WriteArray(0, respons.ToCharArray(), 0, data.Length);
                }
                Console.WriteLine(respons);
            }
        }
    }
}
