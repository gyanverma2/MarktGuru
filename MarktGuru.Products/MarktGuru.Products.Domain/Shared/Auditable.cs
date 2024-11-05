namespace MarktGuru.Products.Domain.Shared
{
    public abstract class Auditable : IEntity
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
