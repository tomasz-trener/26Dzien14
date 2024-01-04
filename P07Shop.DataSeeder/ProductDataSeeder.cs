using Bogus;
using P06Shop.Shared;

namespace P07Shop.DataSeeder
{
    public class ProductDataSeeder
    {
        public static List<Product> GenerateProductData()
        {
            var productFaker = new Faker<Product>()
                 .RuleFor(p => p.Id, f => f.IndexFaker)
                 .RuleFor(p => p.Title, f => f.Commerce.ProductName())
                 .RuleFor(p => p.Description, f => f.Commerce.ProductDescription());

            var products = productFaker.Generate(10);
            return products;
        }
    }
}
