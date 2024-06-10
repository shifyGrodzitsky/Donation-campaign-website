using ChineseSaleServer.Dal;
using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public interface IGiftDal
    {
        Task<List<Gift>> GetAsync();
        Task<Gift> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
        Task AddGiftAsync(Gift gift);
        Task<bool> UpdateGiftAsync(Gift updatedGift);
        Task<List<Gift>> GetGiftByFilterAsync(string filterType, string filterValue);
        Task<List<Gift>> SortGiftsByPriceAsync();
        Task<List<Gift>> SortGiftsByPriceDescAsync();
        Task<List<Gift>> SortGiftsByCategoryAsync(string category);

    }
}
