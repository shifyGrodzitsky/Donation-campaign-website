using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public interface IDraftDal
    {
        Task<List<Draft>> GetAsync(int orderId);
        Task<Draft> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
        Task AddDraftAsync(Draft draft);
        Task<bool> UpdateDraftAsync(Draft draft);
        Task DecreaseDraftQuentityAsync(int draftId, int decrement);
        Task UpdateDraftQuentityAsync(int draftId, int increment);
        Task<Draft> GetDraftByGiftIdAsync(Draft draft);
    }
}


