using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public interface IOrderDetailsService
    {
        Task<List<OrderDetails>> GetOrderDetails();
        Task<OrderDetails> GetOrderDetailsById(int id);
        Task<bool> DeleteOrderDetailsById(int orderDetailsId);
        Task AddOrderDetails(OrderDetails orderDetails);
        Task<bool> UpdateOrderDetails(OrderDetails orderDetails);
        Task ConfirmOrder(int cartId, int userId);
    }
}
