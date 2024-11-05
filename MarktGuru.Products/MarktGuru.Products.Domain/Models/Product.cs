using MarktGuru.Products.Domain.Shared;
using System.Text.Json.Serialization;

namespace MarktGuru.Products.Domain.Models
{
    public class Product : Auditable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }

        public virtual List<ProductPrice>? Prices { get; set; }
    }
}
