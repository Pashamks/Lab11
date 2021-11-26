using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LinuxServer.Models;

namespace LinuxServer.DataManagers
{
    public class RatesManager
    {
        private static byte[] data;
        public static string CheckRates()
        {
            var webRequest = WebRequest.Create("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json") as HttpWebRequest;
            if (webRequest == null)
            {
                return String.Empty;
            }
            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";
            string toFile = "";
            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var contributorsAsJson = sr.ReadToEnd();
                    var val = JsonConvert.DeserializeObject<List<Rates>>(contributorsAsJson);
                    foreach (var item in val)
                    {
                        toFile += item + "\n"; 
                    }
                }
            }
            byte[] data = Encoding.UTF8.GetBytes(toFile);
            
            Console.WriteLine(toFile);
            return toFile;
        }
    }
}