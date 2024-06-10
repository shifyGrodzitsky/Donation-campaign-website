using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public interface ICartDal
    {
        Task<Cart> GetCartByUserIdAsync(int userId);
        Task ResetCartTotalAsync(int cartId);
        Task AddAmountToCartTotalAsync(int cartId, float amount);
    }
}
