
namespace Server.Models
{
    public class Rates
    {
        public string r030 { get; set; }
        public string txt { get; set; }
        public string rate { get; set; }
        public string cc { get; set; }
        public string exchangedate { get; set; }
        public override string ToString()
        {
            return cc + " " + rate;
        }
    }
}
