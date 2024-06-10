using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public interface ICartService
    {
        Task<Cart> GetCartByUserId(int userId);
    }
}
