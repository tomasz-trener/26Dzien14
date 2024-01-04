using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastWPF.Client.Confguration
{
    internal class ProductEndpoint
    {
        public string BaseUrl { get; set; }
        public string GetProducts { get; set; }
        public string CreateProduct { get; internal set; }
        public string? UpdateProduct { get; internal set; }
    }
}
