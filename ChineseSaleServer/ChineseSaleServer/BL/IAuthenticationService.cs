using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public interface IAuthenticationService
    {
        Task<string> Login(string email, string password);
        Task Register(User user);
    }
}
