using ChineseSaleServer.Dal;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ChineseSaleServer.DAL
{
    public class GiftDal : IGiftDal
    {
        private readonly ChineseSaleContext _chineseSaleContext;
        public GiftDal(ChineseSaleContext chineseSaleContext)
        {
                this._chineseSaleContext = chineseSaleContext ?? throw new ArgumentNullException(nameof(chineseSaleContext));
        }


        //get
        public async Task<List<Gift>> GetAsync()
        {
            return await _chineseSaleContext.Gifts.ToListAsync();
        }
        //getById
        public async Task<Gift> GetByIdAsync(int id)
        {
            return await _chineseSaleContext.Gifts.FindAsync(id);
        }

        //delete
        public async Task<bool> DeleteByIdAsync(int id)
        {
            var gift = await _chineseSaleContext.Gifts.FindAsync(id);
            if (gift == null)
            {
                return false; // אם המתנה לא נמצאה במסד הנתונים
            }

            _chineseSaleContext.Gifts.Remove(gift);
            await _chineseSaleContext.SaveChangesAsync();
            return true; // המתנה נמחקה בהצלחה
        }

        //add
        public async Task AddGiftAsync(Gift newGift)
        {
            _chineseSaleContext.Gifts.Add(newGift);
            await _chineseSaleContext.SaveChangesAsync();
        }

        //update
        public async Task<bool> UpdateGiftAsync(Gift updatedGift)
        {
            var existingGift = await _chineseSaleContext.Gifts.FindAsync(updatedGift.Id);
            if (existingGift == null)
            {
                return false; // אם המתנה לא נמצאה במסד הנתונים
            }

            existingGift.Name = updatedGift.Name;
            existingGift.Description = updatedGift.Description;
            existingGift.TicketPrice = updatedGift.TicketPrice;
            existingGift.Category = updatedGift.Category;
            existingGift.DonorId= updatedGift.DonorId;  
            existingGift.NumOfPurchases= updatedGift.NumOfPurchases;

            await _chineseSaleContext.SaveChangesAsync();
            return true; // המתנה עודכנה בהצלחה
        }
        //סינונים
        public async Task<List<Gift>> GetGiftByFilterAsync(string filterType, string filterValue)
        {
            List<Gift> filteredGifts;

            switch (filterType.ToLower())
            {
                case "name":
                    filteredGifts = await _chineseSaleContext.Gifts
                                                .Where(g => g.Name == filterValue)
                                                .ToListAsync();
                    break;
                case "donor":
                    filteredGifts = await _chineseSaleContext.Gifts
                                         .Where(g => _chineseSaleContext.Donors.Any(d => d.Id == g.DonorId && d.FirstName == filterValue)).ToListAsync();
                    break;
                case "numofpurchase":
                    filteredGifts = await _chineseSaleContext.Gifts
                                                  .Where(g => g.NumOfPurchases.ToString() == filterValue)
                                                  .ToListAsync();

                    break;

                default:
                    filteredGifts = null; // אם האופציה לא תקינה, נחזיר ערך ריק
                    break;
            }

            return filteredGifts;
        }


        //sort for user!
        public async Task<List<Gift>> SortGiftsByPriceAsync()
        {
            var sortedGifts = await _chineseSaleContext.Gifts.OrderBy(g => g.TicketPrice).ToListAsync();
            return sortedGifts;
        } 
        public async Task<List<Gift>> SortGiftsByPriceDescAsync()
        {
            var sortedGifts = await _chineseSaleContext.Gifts.OrderByDescending(g => g.TicketPrice).ToListAsync();
            return sortedGifts;
        }

        public async Task<List<Gift>> SortGiftsByCategoryAsync(string category)
        {
            var sortedGifts = await _chineseSaleContext.Gifts.Where(g => g.Category==category).ToListAsync();
            return sortedGifts;
        }
     

    }



}
