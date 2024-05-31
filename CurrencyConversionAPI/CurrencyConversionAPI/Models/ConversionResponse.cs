using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CurrencyConversionAPI.Models
{
    public class ConversionResponse
    {
        [JsonProperty("conversion_rate")]
        public double ConversionRate { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("conversion_rates")]
        public Dictionary<string, double> ConversionRates { get; set; }





    }
}
