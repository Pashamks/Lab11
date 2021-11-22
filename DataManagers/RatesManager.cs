using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Server.Models;

namespace Server.DataManagers
{
    public class RatesManager
    {
        public static void CheckRates(MemoryMappedFile memoryMappedFile)
        {
            var webRequest = WebRequest.Create("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json") as HttpWebRequest;
            if (webRequest == null)
            {
                return;
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
            using (MemoryMappedViewAccessor accessor = memoryMappedFile.CreateViewAccessor())
            {
                accessor.WriteArray(0, toFile.ToCharArray(), 0, data.Length);
            }
            Console.WriteLine(toFile);
        }
    }
}
