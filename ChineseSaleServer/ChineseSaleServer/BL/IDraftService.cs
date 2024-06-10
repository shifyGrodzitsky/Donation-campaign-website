using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public interface IDraftService
    {
        Task<List<Draft>> Get(int orderId);
        Task<Draft> GetById(int id);
        Task<bool> DeleteById(int id);
        Task AddDraft(Draft draft);
        Task<bool> UpdateDraft(Draft draft);
        Task DecreaseDraftQuentity(int draftId);
        Task UpdateDraftQuentity(int draftId);
    }
}
