using MarktGuru.Products.Common.Enums;

namespace MarktGuru.Products.Domain.Models
{
    public class SourceType
    {
        public SourceTypeId Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
