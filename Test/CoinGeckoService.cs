using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private const string ApiKey = "YOUR_API_KEY_HERE "; // Замініть це на свій ключ API

        public async Task<List<Currency>> GetTopCurrenciesAsync(int limit = 10)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "Test-Task"); // Замініть "YourAppName" на назву вашого додатку

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
        public async Task<List<Tuple<DateTime, double>>> GetHistoricalPricesAsync(string currencyId, int days = 1)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "Test-Task");

                // Формування URL-запиту з використанням введених параметрів
                string apiUrl = $"{ApiBaseUrl}/coins/{currencyId.ToLower()}/market_chart";
                var parameters = new Dictionary<string, string>
            {
                {"vs_currency", "usd"},
                {"days", days.ToString()}
            };

                var response = await client.GetAsync($"{apiUrl}?{string.Join("&", parameters.Select(kv => $"{kv.Key}={kv.Value}"))}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    // Розбір відповіді та повернення списку Tuple<DateTime, double>
                    JObject responseObject = JObject.Parse(content);
                    var prices = responseObject["prices"].ToObject<List<List<double>>>();
                    Console.WriteLine($"API Response: {content}");
                    List<Tuple<DateTime, double>> historicalPrices = prices.Select(p => new Tuple<DateTime, double>(UnixTimeStampToDateTime(p[0]), p[1])).ToList();
                    return historicalPrices;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return null;
                }
            }
        }

        // Допоміжний метод для перетворення часу Unix у DateTime
        private DateTime UnixTimeStampToDateTime(double unixTimeStampMillis)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(unixTimeStampMillis).ToLocalTime();
        }
        public async Task<double> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "Test-Task");

                string apiUrl = $"{ApiBaseUrl}/simple/price";
                var parameters = new Dictionary<string, string>
        {
            {"ids", fromCurrency},
            {"vs_currencies", toCurrency}
        };

                var response = await client.GetAsync($"{apiUrl}?{string.Join("&", parameters.Select(kv => $"{kv.Key}={kv.Value}"))}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JObject responseObject = JObject.Parse(content);

                    // Check if the response contains the 'fromCurrency' and 'toCurrency' keys
                    if (responseObject.ContainsKey(fromCurrency.ToLower()) && responseObject[fromCurrency.ToLower()].ToObject<JObject>().ContainsKey(toCurrency.ToLower()))
                    {
                        // Parse the response and get the exchange rate
                        double exchangeRate = responseObject[fromCurrency.ToLower()][toCurrency.ToLower()].ToObject<double>();
                        return exchangeRate;
                    }
                    else
                    {
                        Console.WriteLine($"Error: One or both currencies not found in the response.");
                        return 0.0; // Handle the error case appropriately
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return 0.0; // Handle the error case appropriately
                }
            }
        }
        public async Task<List<CurrencyMarket>> GetCurrencyMarketsAsync(string currencyId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "Test-Task");

                string apiUrl = $"{ApiBaseUrl}/coins/{currencyId.ToLower()}/tickers";

                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    JObject responseObject = JObject.Parse(content);

                    if (responseObject["tickers"] != null)
                    {
                        var tickers = responseObject["tickers"];
                        List<CurrencyMarket> markets = new List<CurrencyMarket>();

                        foreach (var ticker in tickers)
                        {
                            var marketName = ticker["market"]["name"].ToString();
                            var lastPrice = Convert.ToDouble(ticker["last"]);

                            var market = new CurrencyMarket(marketName, lastPrice);
                            markets.Add(market);
                        }

                        return markets;
                    }
                    else
                    {

                        Console.WriteLine("Error: 'tickers' property not found in the API response.");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return null;
                }
            }
        }
    }
}
