using CurrencyConversionAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Buffers.Text;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace CurrencyConversionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConversionController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        string apiKey = "73872d68a6163dac86f576a3";
        string baseAddress = "https://v6.exchangerate-api.com/v6";
        public CurrencyConversionController(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }


        [HttpGet("convert/{fromCurrency}/{toCurrency}/{amount}")]

        public async Task<IActionResult> Convert(string fromCurrency, string toCurrency, double amount)
        {

            string endpoint = $"pair/{fromCurrency}/{toCurrency}/{amount}";
            string fullPath = $"{baseAddress}/{apiKey}/{endpoint}";

            var uri = new Uri(fullPath);

            HttpResponseMessage responseMessage = await _httpClient.GetAsync(uri);

            if (responseMessage.IsSuccessStatusCode)
            {
                string response = await _httpClient.GetStringAsync(uri);



                var conversionResponse = JsonConvert.DeserializeObject<ConversionResponse>(response);




                double convertedAmount = conversionResponse.ConversionRate * amount;

                string message = $"Conversion Rate : {conversionResponse.ConversionRate}\n{amount} {fromCurrency.ToUpper()} = {convertedAmount} {toCurrency.ToUpper()}";


                return Ok(message);


            }
            else
            {
                return BadRequest("Invalid input");
            }




        }


        [HttpGet("ShowLatestExchangeRates/{currency}")]
        public async Task<IActionResult> ShowLatestExchangeRates(string currency)
        {

            string endpoint = $"latest/{currency}";
            string fullPath = $"{baseAddress}/{apiKey}/{endpoint}";

            var uri = new Uri(fullPath);

            HttpResponseMessage responseMessage = await _httpClient.GetAsync(uri);

            if (responseMessage.IsSuccessStatusCode)
            {
                string response = await _httpClient.GetStringAsync(uri);

                var conversionResponse = JsonConvert.DeserializeObject<ConversionResponse>(response);

                Dictionary<string, double> conversionRates = conversionResponse.ConversionRates;


                string message = "";

                foreach (var item in conversionRates)
                {
                    message += $"{item.Key} : {item.Value}\n";

                }
                return Ok(message);


            }
            else
            {
                return BadRequest("Invalid input");
            }




        }



    }


}