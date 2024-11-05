using MarktGuru.Products.Application.Handlers.Products.Commands;
using MarktGuru.Products.Common.Wrapper;
using MarktGuru.Products.Domain.Models;

namespace MarktGuru.Products.Application.Managers.Products
{
    public interface IProductManager
    {
        public Task<PaginatedResult<Product>> GetProductsAsync(int pageNumber, int pageSize);
        public Task<Product> GetProductByIdAsync(int id);
        public Task<Product> CreateProductAsync(CreateProductCommand product);
    }
}
