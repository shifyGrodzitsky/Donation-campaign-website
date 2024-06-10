using ChineseSaleServer.Dal;
using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public class DraftDal:IDraftDal
    {
        private readonly ChineseSaleContext _chineseSaleContext;
        public DraftDal(ChineseSaleContext chineseSaleContext)
        {
            this._chineseSaleContext = chineseSaleContext ?? throw new ArgumentNullException(nameof(chineseSaleContext));
        }


        //get
        public async Task<List<Draft>> GetAsync(int cartId)
        {
            return await _chineseSaleContext.Drafts.Where(d=>d.CartId==cartId).ToListAsync();
        }
        //getById
        public async Task<Draft> GetByIdAsync(int id)
        {
            return await _chineseSaleContext.Drafts.FindAsync(id);
        }

        //delete
        public async Task<bool> DeleteByIdAsync(int id)
        {
            var draft = await _chineseSaleContext.Drafts.FindAsync(id);
            if (draft == null)
            {
                return false; // אם המתנה לא נמצאה במסד הנתונים
            }

            _chineseSaleContext.Drafts.Remove(draft);
            await _chineseSaleContext.SaveChangesAsync();
            return true; // המתנה נמחקה בהצלחה
        }

        //add
        public async Task AddDraftAsync(Draft newDraft)
        {
            _chineseSaleContext.Drafts.Add(newDraft);
            await _chineseSaleContext.SaveChangesAsync();
        }

        //update
        public async Task<bool> UpdateDraftAsync(Draft updatedDraft)
        {
            var existingDraft = await _chineseSaleContext.Drafts.FindAsync(updatedDraft.Id);
            if (existingDraft == null)
            {
                return false; // אם המתנה לא נמצאה במסד הנתונים
            }

            existingDraft.Quentity = updatedDraft.Quentity;
            existingDraft.CartId = updatedDraft.CartId;
            existingDraft.GiftId = updatedDraft.GiftId;

            await _chineseSaleContext.SaveChangesAsync();
            return true; // המתנה עודכנה בהצלחה
        }

    public async Task UpdateDraftQuentityAsync(int draftId, int increment)
    {
        var draft = await _chineseSaleContext.Drafts.FindAsync(draftId);

        if (draft != null)
        {
            draft.Quentity += increment;
            await _chineseSaleContext.SaveChangesAsync();
        }
    }

    public async Task DecreaseDraftQuentityAsync(int draftId, int decrement)
    {
        var draft = await _chineseSaleContext.Drafts.FindAsync(draftId);

        if (draft != null)
        {
            draft.Quentity -= decrement;
            await _chineseSaleContext.SaveChangesAsync();
        }
    }

        public async Task<Draft> GetDraftByGiftIdAsync(Draft draft)
        {
            return await _chineseSaleContext.Drafts.FirstOrDefaultAsync(d => draft.GiftId == d.GiftId && draft.CartId==d.CartId);
        }



    }
}
