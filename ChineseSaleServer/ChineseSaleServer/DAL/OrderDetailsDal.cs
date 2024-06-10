using ChineseSaleServer.Dal;
using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public class OrderDetailsDal : IOrderDetailsDal
    {
        private readonly ChineseSaleContext _chineseSaleContext;
        public OrderDetailsDal(ChineseSaleContext chineseSaleContext)
        {
            this._chineseSaleContext = chineseSaleContext ?? throw new ArgumentNullException(nameof(chineseSaleContext));
        }


        //get
        public async Task<List<OrderDetails>> GetAsync()
        {
            return await _chineseSaleContext.OrderDetails.ToListAsync();
        }
        //getById
        public async Task<OrderDetails> GetByIdAsync(int id)
        {
            return await _chineseSaleContext.OrderDetails.FindAsync(id);
        }

        //delete
        public async Task<bool> DeleteByIdAsync(int id)
        {
            var orderDetails = await _chineseSaleContext.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return false; // אם המתנה לא נמצאה במסד הנתונים
            }

            _chineseSaleContext.OrderDetails.Remove(orderDetails);
            await _chineseSaleContext.SaveChangesAsync();
            return true; // המתנה נמחקה בהצלחה
        }

        //add
        public async Task AddOrderDetailsAsync(OrderDetails newOrderDetails)
        {
            await _chineseSaleContext.OrderDetails.AddAsync(newOrderDetails);
            await _chineseSaleContext.SaveChangesAsync();
        }

        //update
        public async Task<bool> UpdateOrderDetailsAsync(OrderDetails updatedOrderDetails)
        {
            var existingOrderDetails = await _chineseSaleContext.OrderDetails.FindAsync(updatedOrderDetails.Id);
            if (existingOrderDetails == null)
            {
                return false; // אם המתנה לא נמצאה במסד הנתונים
            }

            existingOrderDetails.Quentity = updatedOrderDetails.Quentity;
            existingOrderDetails.OrderId = updatedOrderDetails.OrderId;
            existingOrderDetails.GiftId = updatedOrderDetails.GiftId;

            await _chineseSaleContext.SaveChangesAsync();
            return true; // המתנה עודכנה בהצלחה
        }
    }
}
