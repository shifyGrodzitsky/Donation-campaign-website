using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public interface IPurchasesService
    {
        Task<List<Gift>> SortByExpensiveGift();
        Task<List<Gift>> SortByNumOfPurchases();
        Task<List<User>> PurchasesDetails(int giftId);
        Task<List<OrderDetails>> PurchasesOrderDetails(int giftId);
    }
}
