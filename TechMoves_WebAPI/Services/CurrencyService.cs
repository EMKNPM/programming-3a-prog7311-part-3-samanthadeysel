using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TechMoves_WebAPI.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public CurrencyService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["CurrencyApi:ApiKey"];
            _baseUrl = config["CurrencyApi:BaseUrl"];
        }

        public async Task<decimal> GetUsdToZarRateAsync()
        {
            var url = $"{_baseUrl}/{_apiKey}/latest/USD";

            var response = await _httpClient.GetFromJsonAsync<ExchangeRateResponse>(url);

            if (response?.ConversionRates != null &&
                response.ConversionRates.TryGetValue("ZAR", out var rate))
            {
                return rate;
            }

            throw new InvalidOperationException("Unable to retrieve USD→ZAR rate from API.");
        }

    }

    public class ExchangeRateResponse
    {
        [JsonPropertyName("conversion_rates")]
        public Dictionary<string, decimal> ConversionRates { get; set; } = new();
    }
}
