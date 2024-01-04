using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using P04WeatherForecastWPF.Client.Confguration;
using P06Shop.Shared;
using P06Shop.Shared.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastWPF.Client.Services
{
    internal class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public ProductService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        public async Task<ServiceReponse<List<Product>>> GetProductsAsync()
        {
            var response= await _httpClient.GetAsync(_appSettings.ProductEndpoint.GetProducts);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceReponse<List<Product>>>(json);
            return result;
        }
    }
}
