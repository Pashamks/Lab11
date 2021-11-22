using Server.Models;

namespace Server.Models
{
    public class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }
        public WindInfo Wind { get; set; }
        public float Visibility { get; set; }
        public string Name { get; set; }
    }
}
