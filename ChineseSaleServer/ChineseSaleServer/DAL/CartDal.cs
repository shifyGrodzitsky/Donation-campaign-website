using ChineseSaleServer.Dal;
using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public class CartDal: ICartDal
    {
        private readonly ChineseSaleContext _chineseSaleContext;

        public CartDal(ChineseSaleContext chineseSaleContext)
        {
                this._chineseSaleContext = chineseSaleContext ?? throw new ArgumentNullException(nameof(chineseSaleContext));

        }


        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            // בדיקה אם יש סל קיים למשתמש
            var existingCart = await _chineseSaleContext.Carts.FirstOrDefaultAsync(c => c.UserID == userId);


            if (existingCart == null)
            {
                // יצירת סל חדש אם לא קיים סל
                var newCart = new Cart { UserID = userId, Total = 0 };
                _chineseSaleContext.Carts.Add(newCart);
                await _chineseSaleContext.SaveChangesAsync();

                return newCart;
            }
            else
            {
                // חזרה על סל קיים אם קיים
                return existingCart;
            }
        }
        //public async Task<Cart> GetCartByUserIdAsync(int userId)
        //{
        //    //var existingCart = await _chineseSaleContext.Carts.FirstOrDefaultAsync(c => c.UserID == userId);

        //    if (_chineseSaleContext.Carts.Any(c => c.UserID == userId))
        //    {
        //        // חזרה על סל קיים אם קיים
        //        return await _chineseSaleContext.Carts.FirstOrDefaultAsync(c => c.UserID == userId);
        //    }
        //    else
        //    {
        //        // יצירת סל חדש אם לא קיים סל
        //        var newCart = new Cart { UserID = userId, Total = 0 };
        //        _chineseSaleContext.Carts.Add(newCart);
        //        await _chineseSaleContext.SaveChangesAsync();
        //        return newCart;
        //    }

        //}


        public async Task ResetCartTotalAsync(int cartId)
        {
            var cart = await _chineseSaleContext.Carts.FindAsync(cartId);

            if (cart != null)
            {
                cart.Total = 0;
                await _chineseSaleContext.SaveChangesAsync();
            }
        }


        public async Task AddAmountToCartTotalAsync(int cartId, float amount)
        {
            var cart = await _chineseSaleContext.Carts.FindAsync(cartId);

            if (cart != null)
            {
                cart.Total += amount;
                await _chineseSaleContext.SaveChangesAsync();
            }
        }

    }
}


