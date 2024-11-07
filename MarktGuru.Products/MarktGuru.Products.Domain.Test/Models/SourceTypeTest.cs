using MarktGuru.Products.Common.Enums;
using MarktGuru.Products.Domain.Models;
using MarktGuru.Products.Common.Extensions;
using Xunit;

namespace MarktGuru.Products.Domain.Test.Models
{
    public class SourceTypeTest
    {
        private const string Name = "Web";
        private readonly SourceTypeId Id = SourceTypeId.Web;
        [Fact]
        public void Constructor_Test()
        {
            var sourceType = new SourceType()
            {
                Id = Id,
                Name = Name
            };
            Assert.Equal(Id, sourceType.Id);
            Assert.Equal(Name,sourceType.Id.GetDisplayName());
        }
    }
}
