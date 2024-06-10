using ChineseSaleServer.DAL;
using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public class CartService : ICartService
    {
        private readonly ICartDal _cartDal;
     

        public CartService(ICartDal cartDal)
         {
            this._cartDal = cartDal ?? throw new ArgumentNullException(nameof(cartDal));
        }
        
        public async Task<Cart> GetCartByUserId(int userId)
        {
          return  await _cartDal.GetCartByUserIdAsync(userId);
        }
    }
}
