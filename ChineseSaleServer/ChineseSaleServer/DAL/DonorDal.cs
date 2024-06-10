using ChineseSaleServer.Dal;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace ChineseSaleServer.DAL
{
    public class DonorDal : IDonorDal
    {
        private readonly ChineseSaleContext _chineseSaleContext;
        public DonorDal(ChineseSaleContext chineseSaleContext)
        {
                this._chineseSaleContext = chineseSaleContext ?? throw new ArgumentNullException(nameof(chineseSaleContext));
        }


        //get
        public async Task<List<Donor>> GetAsync()
        {
            return await _chineseSaleContext.Donors.ToListAsync();
        }
        //getById
        public async Task<Donor> GetByIdAsync(int id)
        {
            return await _chineseSaleContext.Donors.FindAsync(id);
        }

        //delete
        public async Task<bool> DeleteByIdAsync(int id)
        {
            var donor = await _chineseSaleContext.Donors.FindAsync(id);
            if (donor == null)
            {
                return false; // אם המתנה לא נמצאה במסד הנתונים
            }

            _chineseSaleContext.Donors.Remove(donor);
            await _chineseSaleContext.SaveChangesAsync();
            return true; // המתנה נמחקה בהצלחה
        }

        //add
        public async Task AddDonorAsync(Donor newDonor)
        {
            _chineseSaleContext.Donors.Add(newDonor);
            await _chineseSaleContext.SaveChangesAsync();
        }

        //update
        public async Task<bool> UpdateDonorAsync(Donor updatedDonor)
        {
            var existingDonor = await _chineseSaleContext.Donors.FindAsync(updatedDonor.Id);
            if (existingDonor == null)
            {
                return false; // אם המתנה לא נמצאה במסד הנתונים
            }

            existingDonor.FirstName = updatedDonor.FirstName;
            existingDonor.LastName = updatedDonor.LastName;
            existingDonor.Email = updatedDonor.Email;
            existingDonor.Address = updatedDonor.Address;
            existingDonor.Phone = updatedDonor.Phone;

            await _chineseSaleContext.SaveChangesAsync();
            return true; // המתנה עודכנה בהצלחה
        }


        //to each donor his gifts

        public async Task<List<Gift>> GetGiftsByDonorAsync(int donorId)
        {
            var gifts = await _chineseSaleContext.Gifts
                                    .Where(g => g.DonorId == donorId)
                                    .ToListAsync();

            return gifts;
        }


        //סינונים
        public async Task<Donor> GetDonorByFilterAsync(string filterType, string filterValue)
        {
            Donor filteredDonor;

            switch (filterType.ToLower())
            {
                case "name":
                    filteredDonor = await _chineseSaleContext.Donors
                                                .FirstOrDefaultAsync(d => d.FirstName == filterValue);
                    break;
                case "email":
                    filteredDonor = await _chineseSaleContext.Donors
                                                .FirstOrDefaultAsync(d => d.Email == filterValue);
                    break;
                case "gift":
                    filteredDonor= await _chineseSaleContext.Donors
                                         .Where(d => _chineseSaleContext.Gifts.Any(g => g.DonorId == d.Id && g.Name == filterValue)).FirstOrDefaultAsync();

                     break;

                default:
                    filteredDonor = null; // אם האופציה לא תקינה, נחזיר ערך ריק
                    break;
            }

            return filteredDonor;
        }
    } 


}
