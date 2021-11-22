using Newtonsoft.Json;
using Server.Models;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Net;
using System.Text;

namespace Server.DataManagers
{
    public class WeatherManager
    {
        public static void  CheckWeather(MemoryMappedFile memoryMappedFile)
        {
            using (WebClient wc = new WebClient())
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?q=Lviv&units=metric&appid=74372e553fdda3a188f09f0ec7b2bd72");
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string respons;
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    respons = streamReader.ReadToEnd();
                }
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(respons);
                string toFile = $"Weather forecast for \"{weatherResponse.Name}\":\n" +
                        $"Temperature : \n\treal : {weatherResponse.Main.Temp} " +
                        $"\n\tmin : {weatherResponse.Main.Temp_min} " +
                        $"\n\tmax : {weatherResponse.Main.Temp_max} " +
                        $"\nPressure : {weatherResponse.Main.Pressure}" +
                        $"\nHumidity : {weatherResponse.Main.Humidity}" +
                        $"\nVisibility : {weatherResponse.Visibility}" +
                        $"\nWind:" +
                        $"\n\tspeed : {weatherResponse.Wind.Speed}" +
                        $"\n\tdeg : {weatherResponse.Wind.Deg}" +
                        $"\n\tgust : {weatherResponse.Wind.Gust}";
                byte[] data = Encoding.UTF8.GetBytes(toFile);
                using (MemoryMappedViewAccessor accessor = memoryMappedFile.CreateViewAccessor())
                {
                    accessor.WriteArray(0, toFile.ToCharArray(), 0, data.Length);
                }
                Console.WriteLine(toFile);
            }
        }
    }
}
