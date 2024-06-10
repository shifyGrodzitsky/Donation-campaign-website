using ChineseSaleServer.DAL;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChineseSaleServer.BL
{
    public class DonorService : IDonorService
    {
        private readonly IDonorDal _donorDal;

        public DonorService(IDonorDal donorDal)
        {
            this._donorDal = donorDal?? throw new ArgumentNullException(nameof(DonorDal));
        }

        public async Task AddDonor(Donor donor)
        {
           await _donorDal.AddDonorAsync(donor);
        }

        public async Task<bool> DeleteDonorById(int donorId)
        {
            return await _donorDal.DeleteByIdAsync(donorId);
        }

        public async Task<Donor> GetDonorById(int id)
        {
            return await _donorDal.GetByIdAsync(id);
        }

        public async Task<List<Donor>> GetDonors()
        {
            return await _donorDal.GetAsync();
        }

        public async Task<bool> UpdateDonor(Donor donor)
        {
            return await _donorDal.UpdateDonorAsync(donor);
        }

        public async Task<List<Gift>> GetGiftsByDonor(int donorId)
        {
            return await _donorDal.GetGiftsByDonorAsync(donorId);   
        }

        public async Task<Donor> GetDonorByFilter(string filterType, string filterValue)
        {
            return await _donorDal.GetDonorByFilterAsync(filterType, filterValue);  
        }
    }
}
