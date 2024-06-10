using ChineseSaleServer.DAL;
using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public class PurchasesService : IPurchasesService
    {
        private readonly IPurchasesDal _purchasesDal;

        public PurchasesService(IPurchasesDal purchasesDal)
        {
            this._purchasesDal = purchasesDal ?? throw new ArgumentNullException(nameof(purchasesDal));
        }
        public async Task<List<User>> PurchasesDetails(int giftId)
        {
            return await _purchasesDal.PurchasesDetailsAsync(giftId);
        }

        public async Task<List<OrderDetails>> PurchasesOrderDetails(int giftId)
        {
            return await _purchasesDal.PurchasesOrderDetailsAsync(giftId);
        }

        public async Task<List<Gift>> SortByExpensiveGift()
        {
            return await _purchasesDal.SortByExpensiveGiftAsync();
        }

        public async Task<List<Gift>> SortByNumOfPurchases()
        {
            return await _purchasesDal.SortByNumOfPurchasesAsync();
        }
    }
}
