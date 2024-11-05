using MarktGuru.Products.Common.Enums;
using MarktGuru.Products.Common.Wrapper;
using MarktGuru.Products.Domain.Models;

namespace MarktGuru.Products.Application.Managers.Products
{
    public interface IProductManager
    {
        public Task<PaginatedResult<Product>> GetProductsAsync(int pageNumber, int pageSize);
    }
}
