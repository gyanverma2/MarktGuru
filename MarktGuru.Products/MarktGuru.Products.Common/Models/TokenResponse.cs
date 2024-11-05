namespace MarktGuru.Products.Common.Models
{
    public class TokenResponse
    {
        public string AccessToken { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
