using Server.Models;

namespace Server.Models
{
    public class TemperatureInfo
    {
        public float Temp { get; set; }
        public float Temp_min { get; set; }
        public float Temp_max { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }
    }
}
