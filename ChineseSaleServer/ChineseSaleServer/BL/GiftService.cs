using ChineseSaleServer.DAL;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChineseSaleServer.BL
{
    public class GiftService : IGiftService
    {
        private readonly IGiftDal _giftDal;

        public GiftService(IGiftDal giftDal)
        {
            this._giftDal = giftDal ?? throw new ArgumentNullException(nameof(GiftDal));
        }
        public async Task<List<Gift>> GetGifts()
        {
            return await _giftDal.GetAsync();

        }
        public async Task<Gift> GetGiftById(int id)
        {
            return await _giftDal.GetByIdAsync(id);
        }
        public async Task<bool> DeleteGiftById(int giftId)
        {
            return await _giftDal.DeleteByIdAsync(giftId);
        }
        public async Task AddGift(Gift gift)
        {
            await this._giftDal.AddGiftAsync(gift);
        }

        public async Task<bool> UpdateGift(Gift gift)
        {
           return await this._giftDal.UpdateGiftAsync( gift);
        }

        public async Task<List<Gift>> GetGiftByFilter(string filterType, string filterValue)
        {
            return await _giftDal.GetGiftByFilterAsync(filterType, filterValue);
        }

        public async Task<List<Gift>> SortGiftsByPrice()
        {
            return await _giftDal.SortGiftsByPriceAsync();
        }

        public async Task<List<Gift>> SortGiftsByPriceDesc()
        {
            return await _giftDal.SortGiftsByPriceDescAsync();  
        }

        public async Task<List<Gift>> SortGiftsByCategory(string category)
        {
            return  await _giftDal.SortGiftsByCategoryAsync(category);
        }
    }
}
