using ChineseSaleServer.Dal;
using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public interface IPurchasesDal
    {
        Task<List<Gift>> SortByExpensiveGiftAsync();
        Task<List<Gift>> SortByNumOfPurchasesAsync();
        Task<List<User>> PurchasesDetailsAsync(int giftId);
        Task<List<OrderDetails>> PurchasesOrderDetailsAsync(int giftId);
        Task AddPurchaseAsync(int giftId);


    }
}
