using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public interface IDonorService
    {
        Task<List<Donor>> GetDonors();
        Task<Donor> GetDonorById(int id);
        Task<bool> DeleteDonorById(int donorId);
        Task AddDonor(Donor donor);
        Task <bool> UpdateDonor(Donor donor);
        Task<List<Gift>> GetGiftsByDonor(int donorId);
        Task<Donor> GetDonorByFilter(string filterType, string filterValue);
    }
}
