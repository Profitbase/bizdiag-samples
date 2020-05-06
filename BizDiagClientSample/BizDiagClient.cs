using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BizDiagClientSample
{
    public sealed class BizDiagClient
    {        
        private readonly HttpClient _httpClient;

        public BizDiagClient(IHttpClientFactory httpClientFactory, BizDiagClientConfiguration clientConfiguration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://profitbase.azure-api.net/BizDiag/");
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", clientConfiguration.SubscriptionKey);
        }

        public async Task<List<GrossMarginOutput>> CalculateGrossMarginByYearAsync(List<GrossMarginPeriodicInput> input)
        {
            var response = await _httpClient.PostAsync("GrossMargins", new StringContent(JsonSerializer.Serialize(input, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })));

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GrossMarginOutput>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
