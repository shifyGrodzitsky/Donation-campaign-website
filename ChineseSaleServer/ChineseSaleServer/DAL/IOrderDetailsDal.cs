using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public interface IOrderDetailsDal
    {
        Task<List<OrderDetails>> GetAsync();
        Task<OrderDetails> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
        Task AddOrderDetailsAsync(OrderDetails orderDetails);
        Task<bool> UpdateOrderDetailsAsync(OrderDetails updatedOrderDetails);
    }
}
