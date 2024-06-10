using ChineseSaleServer.Dal;
using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public class PurchasesDal:IPurchasesDal
    {

        private readonly ChineseSaleContext _chineseSaleContext;
        public PurchasesDal(ChineseSaleContext chineseSaleContext)
        {
            _chineseSaleContext = chineseSaleContext ?? throw new ArgumentNullException(nameof(chineseSaleContext));
        }

        public async Task<List<Gift>> SortByExpensiveGiftAsync()
        {
            return await _chineseSaleContext.Gifts.OrderByDescending(g => g.TicketPrice).ToListAsync();
        }
            
//        public async Task<List<Gift>> SortGiftsByPriceAsync()
//        {
//            var gifts = await _chineseSaleContext.Gifts
//                .Include(g => _chineseSaleContext.OrderDetails)
//                .Where(g => _chineseSaleContext.OrderDetails.Any())
//                .ToListAsync();

//            return gifts.OrderByDescending(g => _chineseSaleContext.OrderDetails.Max(od => od.Price)).ToList();
//}
        public async Task<List<Gift>> SortByNumOfPurchasesAsync()
        {
            return await _chineseSaleContext.Gifts.OrderByDescending(g => g.NumOfPurchases).ToListAsync();
        }


        public async Task<List<User>> PurchasesDetailsAsync(int giftId)
        {
            return await _chineseSaleContext.Users
                .Where(u => _chineseSaleContext.Orders
                    .Any(o => _chineseSaleContext.OrderDetails
                        .Any(od => od.GiftId == giftId && od.OrderId == o.Id && o.UserID == u.Id)))
                .ToListAsync();
        }




        public async Task<List<OrderDetails>> PurchasesOrderDetailsAsync(int giftId)
        {
            return await _chineseSaleContext.OrderDetails
                .Where(od=> od.GiftId == giftId)
                .ToListAsync();         }


        public async Task AddPurchaseAsync(int giftId)
        {
            var g = await _chineseSaleContext.Gifts.FindAsync(giftId);

            if (g != null)
            {
                g.NumOfPurchases += 1;
                await _chineseSaleContext.SaveChangesAsync();
            }
        }
    }
}
