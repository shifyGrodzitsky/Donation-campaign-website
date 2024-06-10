using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public interface IOrderDal
    {
        Task<List<Order>> GetAsync();
        Task<Order> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
        Task<Order> AddOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(Order updatedOrder);
        Task<User> GetUserByOrderId(int orderId);
    }
}
