using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Server.Models;

namespace Server.DataManagers
{
    public class RatesManager
    {
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
                    }
                }
            }
        }

    }
}
