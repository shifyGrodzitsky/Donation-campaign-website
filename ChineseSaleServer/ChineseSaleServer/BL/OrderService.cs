using ChineseSaleServer.DAL;
using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderService(IOrderDal orderDal)
        {
            this._orderDal = orderDal ?? throw new ArgumentNullException(nameof(orderDal));
        }
        public async Task<List<Order>> GetOrders()
        {
            return await _orderDal.GetAsync();

        }
        public async Task<Order> GetOrderById(int id)
        {
            return await _orderDal.GetByIdAsync(id);
        }
        public async Task<bool> DeleteOrderById(int orderId)
        {
            return await _orderDal.DeleteByIdAsync(orderId);
        }
        public async Task<Order> AddOrder(Order order)
        {
           return await this._orderDal.AddOrderAsync(order);
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            return await this._orderDal.UpdateOrderAsync(order);
        }

   
    }
}
