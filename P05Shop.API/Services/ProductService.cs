using Microsoft.EntityFrameworkCore;
using P05Shop.API.Models;
using P06Shop.Shared;
using P06Shop.Shared.Services.ProductService;
using P07Shop.DataSeeder;

namespace P05Shop.API.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceReponse<List<Product>>> GetProductsAsync()
        {
            var result = new ServiceReponse<List<Product>>();

            try
            {
                result.Data = await _dataContext.Products.ToListAsync();
                result.Success = true;
                result.Message = "Data retrieved successfully";
            }
            catch (Exception ex)
            {
                result.Message = $"Error retrieving data from the database {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceReponse<Product>> CreateProductAsync(Product newProduct)
        {
            var result = new ServiceReponse<Product>();

            try
            {
                await _dataContext.Products.AddAsync(newProduct);
                await _dataContext.SaveChangesAsync();

                result.Data = newProduct;
                result.Success = true;
                result.Message = "Data created successfully";
            }
            catch (Exception ex)
            {
                result.Message = $"Error while creating new product {ex.Message}";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceReponse<Product>> UpdateProductAsync(Product updatedProduct)
        {
            var result = new ServiceReponse<Product>();

            try
            {
                var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);

                if (product != null)
                {
                    product.Title = updatedProduct.Title;
                    product.Description = updatedProduct.Description;
                    product.Barcode = updatedProduct.Barcode;
                    product.Price = updatedProduct.Price;
                    product.ReleaseDate = updatedProduct.ReleaseDate;

                    await _dataContext.SaveChangesAsync();

                    result.Data = product;
                    result.Success = true;
                    result.Message = "Data updated successfully";
                }
                else
                {
                    result.Message = $"Product with id {updatedProduct.Id} not found";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"Error while updating product {ex.Message}";
                result.Success = false;
            }

            return result;
        }


    }
}
