using ChineseSaleServer.Dal;
using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public class OrderDal : IOrderDal
    {
        private readonly ChineseSaleContext _chineseSaleContext;
        public OrderDal(ChineseSaleContext chineseSaleContext)
        {
            this._chineseSaleContext = chineseSaleContext ?? throw new ArgumentNullException(nameof(chineseSaleContext));
        }


        //get
        public async Task<List<Order>> GetAsync()
        {
            return await _chineseSaleContext.Orders.ToListAsync();
        }
        //getById
        public async Task<Order> GetByIdAsync(int id)
        {
            return await _chineseSaleContext.Orders.FindAsync(id);

        }
        //return user
        public async Task<User> GetUserByOrderId(int orderId)
        {
            return await _chineseSaleContext.Users.Where(u => _chineseSaleContext.Orders.Any(o => o.UserID == u.Id && o.Id == orderId)).FirstOrDefaultAsync();   
        }

        //delete
        public async Task<bool> DeleteByIdAsync(int id)
        {
            var order = await _chineseSaleContext.Orders.FindAsync(id);
            if (order == null)
            {
                return false; // אם המתנה לא נמצאה במסד הנתונים
            }

            _chineseSaleContext.Orders.Remove(order);
            await _chineseSaleContext.SaveChangesAsync();
            return true; // המתנה נמחקה בהצלחה
        }

        //add
        public async Task<Order> AddOrderAsync(Order newOrder)
        {
            _chineseSaleContext.Orders.Add(newOrder);
            await _chineseSaleContext.SaveChangesAsync();

            // מציאת ההזמנה החדשה על ידי חיפוש לפי מאפיין שאחראי על יצירת ה-ID באופן אוטומטי
            var addedOrder = await _chineseSaleContext.Orders.SingleOrDefaultAsync(order => order.OrderDate == newOrder.OrderDate && order.UserID == newOrder.UserID);
            return addedOrder;
        }


        //update
        public async Task<bool> UpdateOrderAsync(Order updatedOrder)
        {
            var existingOrder = await _chineseSaleContext.Orders.FindAsync(updatedOrder.Id);
            if (existingOrder == null)
            {
                return false; // אם המתנה לא נמצאה במסד הנתונים
            }

            existingOrder.OrderDate = updatedOrder.OrderDate;
            existingOrder.Total = updatedOrder.Total;
            existingOrder.UserID = updatedOrder.UserID;

            await _chineseSaleContext.SaveChangesAsync();
            return true; // המתנה עודכנה בהצלחה
        }

    }
}
