using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Test
{
    internal class CoinGeckoService
    {
        private const string ApiBaseUrl = "https://api.coingecko.com/api/v3";
        private const string ApiKey = "CG-zUsikNX5cHHRCfmPqiWvpRni"; // Замініть це на свій ключ API

        public async Task<List<Currency>> GetTopCurrenciesAsync(int limit = 10)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "Test-task"); // Замініть "YourAppName" на назву вашого додатку

                string apiUrl = $"{ApiBaseUrl}/coins/markets";
                var parameters = new Dictionary<string, string>
            {
                {"vs_currency", "usd"},
                {"order", "market_cap_desc"},
                {"per_page", limit.ToString()},
                {"page", "1"},
                {"sparkline", "false"}
            };

                // Додайте ключ API як параметр запиту
                parameters.Add("apiKey", ApiKey);

                var response = await client.GetAsync($"{apiUrl}?{string.Join("&", parameters.Select(kv => $"{kv.Key}={kv.Value}"))}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Currency>>(content);
                }
                else
                {
                    // Обробка помилок
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return null;
                }
            }
        }
    }
}