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
        static string text = string.Empty;
        public static void CheckRates()
        {

            var webRequest = WebRequest.Create("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json") as HttpWebRequest;
            if (webRequest == null)
            {
                return;
            }
            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";

            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var contributorsAsJson = sr.ReadToEnd();
                    var val = JsonConvert.DeserializeObject<List<Rates>>(contributorsAsJson);
                    foreach (var item in val)
                    {
                        Console.WriteLine(item);
                        text += item + "\n";
                    }
                }
            }
            using (MemoryMappedFile memoryMappedFile = MemoryMappedFile.CreateNew("RatesFile", 10000))
            using (MemoryMappedViewAccessor accessor = memoryMappedFile.CreateViewAccessor())
            {
                byte[] data = Encoding.UTF8.GetBytes(text);
                accessor.WriteArray(0, text.ToCharArray(), 0, data.Length);
            }
        }

    }
}
