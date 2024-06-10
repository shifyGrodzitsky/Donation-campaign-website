using ChineseSaleServer.DAL;
using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public class DraftService:IDraftService
    {
        private readonly IDraftDal _draftDal;
        private readonly ICartDal _cartDal;
        private readonly IGiftDal _giftDal;

        public DraftService(IDraftDal draftDal, ICartDal cartDal,IGiftDal giftDal)
        {
            this._draftDal = draftDal ?? throw new ArgumentNullException(nameof(draftDal));
            this._cartDal = cartDal;
            this._giftDal = giftDal;
        }

        public async Task AddDraft(Draft draft)
        {
            Draft d = await _draftDal.GetDraftByGiftIdAsync(draft);
            if (d == null) { 
            Gift g = await _giftDal.GetByIdAsync(draft.GiftId);
            await _cartDal.AddAmountToCartTotalAsync( draft.CartId,g.TicketPrice);
            await _draftDal.AddDraftAsync(draft);
            }
            else
            {
                Gift g = await _giftDal.GetByIdAsync(draft.GiftId);
                await _cartDal.AddAmountToCartTotalAsync(draft.CartId, g.TicketPrice);
                await _draftDal.UpdateDraftQuentityAsync(d.Id, 1);
            }

        }

        public async Task DecreaseDraftQuentity(int draftId)
        {
            Draft d = await _draftDal.GetByIdAsync(draftId);
            Gift g = await _giftDal.GetByIdAsync(d.GiftId);
            await _cartDal.AddAmountToCartTotalAsync(d.CartId, g.TicketPrice*(-1));
            await _draftDal.DecreaseDraftQuentityAsync(draftId, 1);
        }

        public Task DecreaseDraftQuentity(int draftId, int decrement)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteById(int id)
        {
            Draft draft = await _draftDal.GetByIdAsync(id);
            Gift g = await _giftDal.GetByIdAsync(draft.GiftId);
            await _cartDal.AddAmountToCartTotalAsync(draft.CartId, (g.TicketPrice*(-1)*draft.Quentity));
            return await _draftDal.DeleteByIdAsync(id);

        }

        public async Task<List<Draft>> Get(int orderId)
        {
            return await _draftDal.GetAsync(orderId);
        }

        public async Task<Draft> GetById(int id)
        {
           return await _draftDal.GetByIdAsync(id);
        }

        public async Task<bool> UpdateDraft(Draft draft)
        {
            return await _draftDal.UpdateDraftAsync(draft);
        }

        public async Task UpdateDraftQuentity(int draftId)
        {
            Draft d = await _draftDal.GetByIdAsync(draftId);
            Gift g = await _giftDal.GetByIdAsync(d.GiftId);
            await _cartDal.AddAmountToCartTotalAsync(d.CartId, g.TicketPrice);
            await _draftDal.UpdateDraftQuentityAsync(draftId,1);
        }

        public Task UpdateDraftQuentity(int draftId, int increment)
        {
            throw new NotImplementedException();
        }
    }
}
