using MarktGuru.Products.Common.Models;

namespace MarktGuru.Products.Application.Services
{
    public interface IAuthService
    {
        TokenResponse Authenticate(string username, string password);
    }
}
