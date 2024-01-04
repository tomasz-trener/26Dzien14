using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using P04WeatherForecastWPF.Client.Confguration;
using P06Shop.Shared;
using P06Shop.Shared.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<ServiceReponse<Product>> CreateProductAsync(Product newProduct)
        {
            var response = await _httpClient.PostAsJsonAsync(_appSettings.ProductEndpoint.CreateProduct, newProduct);
            var result = await response.Content.ReadFromJsonAsync<ServiceReponse<Product>>();
            return result;
        }

        public async Task<ServiceReponse<bool>> DeleteProductAsync(int id)
        {
            // jeżeli uzyjemy / na początku to będzie to adres bezwzględny
            // czyli scieżka bedzie wyglądać tak: https://localhost:5001/1
            // zacznyamy od roota
            var response = await _httpClient.DeleteAsync($"{id}");
            var result = await response.Content.ReadFromJsonAsync<ServiceReponse<bool>>();
            return result;
        }

        public async Task<ServiceReponse<Product>> GetProductAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            var result = await response.Content.ReadFromJsonAsync<ServiceReponse<Product>>();
            return result;
        }

        public async Task<ServiceReponse<List<Product>>> GetProductsAsync()
        {
            var response= await _httpClient.GetAsync(_appSettings.ProductEndpoint.GetProducts);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceReponse<List<Product>>>(json);
            return result;
        }

        public async Task<ServiceReponse<Product>> UpdateProductAsync(Product updatedProduct)
        {
            var response = await _httpClient.PutAsJsonAsync(_appSettings.ProductEndpoint.UpdateProduct, updatedProduct);
            var result = await response.Content.ReadFromJsonAsync<ServiceReponse<Product>>();
            return result;
        }
    }
}
