using Microsoft.AspNetCore.Mvc;
using P06Shop.Shared;
using P06Shop.Shared.Services.ProductService;

namespace P05Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService; // wstrzyknięcie zależności
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
       {
            //try
            //{
            //    // pobranie danych z Serwisu 
            //    //var products = await _productService.GetProducts();
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError,
            //                           $"Error retrieving data from the database {ex.Message}");
            //}

            var result = await _productService.GetProductsAsync();

            if(result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, $"Internal server error {result.Message}");
            }
       }
    }


}
