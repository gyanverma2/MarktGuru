using MarktGuru.Products.Domain.Shared;

namespace MarktGuru.Products.Domain.Models
{
    public class Product : Auditable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
    }
}
