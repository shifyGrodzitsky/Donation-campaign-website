using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(int id);
        Task<bool> DeleteOrderById(int giftId);
        Task<Order> AddOrder(Order order);
        Task<bool> UpdateOrder(Order order);
    }
}
