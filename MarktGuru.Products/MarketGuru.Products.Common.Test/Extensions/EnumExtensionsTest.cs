using MarktGuru.Products.Common.Enums;
using Xunit;
using MarktGuru.Products.Common.Extensions;

namespace MarketGuru.Products.Common.Test.Extensions
{
    public class EnumExtensionsTest
    {
        [Fact]
        public void GetDisplayName_WithDisplayAttribute_ReturnsDisplayName()
        {
            var enumDisplayName = "Web";
            var enumValue = SourceTypeId.Web;

            var displayName = enumValue.GetDisplayName();

            Assert.Equal(enumDisplayName, displayName);
        }
    }
}
