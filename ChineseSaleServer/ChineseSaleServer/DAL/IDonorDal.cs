using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public interface IDonorDal
    {
        Task<List<Donor>> GetAsync();
        Task<Donor> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
        Task AddDonorAsync(Donor donor);
        Task<bool> UpdateDonorAsync(Donor updatedDonor);
        Task<List<Gift>> GetGiftsByDonorAsync(int donorId);
        Task<Donor> GetDonorByFilterAsync(string filterType, string filterValue);

    }
}
