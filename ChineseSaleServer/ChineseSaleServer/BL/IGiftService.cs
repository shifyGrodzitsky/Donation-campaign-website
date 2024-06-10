using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public interface IGiftService
    {
        Task<List<Gift>> GetGifts();
        Task<Gift> GetGiftById(int id);
        Task<bool> DeleteGiftById(int giftId);
        Task AddGift(Gift gift);
        Task <bool> UpdateGift(Gift gift);
        Task<List<Gift>> GetGiftByFilter(string filterType, string filterValue);
        Task<List<Gift>> SortGiftsByPrice();
        Task<List<Gift>> SortGiftsByPriceDesc();
        Task<List<Gift>> SortGiftsByCategory(string category);


    }
}
