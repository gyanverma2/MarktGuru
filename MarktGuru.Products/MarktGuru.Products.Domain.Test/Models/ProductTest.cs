using MarktGuru.Products.Domain.Models;
using Xunit;

namespace MarktGuru.Products.Domain.Test.Models
{
    public class ProductTest
    {
        private const int Id = 1;
        private const string Name = "Product Name";
        private const string Description = "Product Description";
        private const bool IsAvailable = true;

        [Fact]
        public void Constructor_Test()
        {
            var product = new Product()
            {
                Id = Id,
                Name = Name,
                Description =Description,
                IsAvailable = IsAvailable
            };

            Assert.Equal(Id, product.Id);
            Assert.Equal(Name, product.Name);
            Assert.Equal(Description, product.Description);
            Assert.Equal(IsAvailable, product.IsAvailable);
        }
    }
}
