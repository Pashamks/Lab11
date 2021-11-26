using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace LinuxServer.DataManagers
{
    public class StockPricesManager
    {
        const int divider = 6;
        public static string CheckStockPrices()
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
                

                Console.WriteLine(respons);
                return respons;
            }
        }
    }
}