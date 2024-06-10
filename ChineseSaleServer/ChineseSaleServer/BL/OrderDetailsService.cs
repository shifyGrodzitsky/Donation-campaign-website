using ChineseSaleServer.DAL;
using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailsDal _orderDetailsDal;
        private readonly IDraftDal _draftDal;
        private readonly IOrderDal _orderDal;
        private readonly ICartDal _cartDal;
        private readonly IPurchasesDal _purchasesDal;

        public OrderDetailsService(IOrderDetailsDal orderDetailsDal,IDraftDal draftDal,IOrderDal orderDal,ICartDal cartDal,IPurchasesDal purchasesDal)
        {
            this._orderDetailsDal = orderDetailsDal ?? throw new ArgumentNullException(nameof(orderDetailsDal));
            this._draftDal = draftDal ?? throw new ArgumentNullException(nameof(draftDal));
            this._orderDal = orderDal ?? throw new ArgumentNullException(nameof(orderDal));
            this._cartDal = cartDal ?? throw new ArgumentNullException(nameof(cartDal));
            this._purchasesDal = purchasesDal;
        }
        public async Task<List<OrderDetails>> GetOrderDetails()
        {
            return await _orderDetailsDal.GetAsync();

        }
        public async Task<OrderDetails> GetOrderDetailsById(int id)
        {
            return await _orderDetailsDal.GetByIdAsync(id);
        }
        public async Task<bool> DeleteOrderDetailsById(int orderDetailsId)
        {
            return await _orderDetailsDal.DeleteByIdAsync(orderDetailsId);
        }
        public async Task AddOrderDetails(OrderDetails orderDetails)
        {
            await this._orderDetailsDal.AddOrderDetailsAsync(orderDetails);
        }

        public async Task<bool> UpdateOrderDetails(OrderDetails orderDetails)
        {
            return await this._orderDetailsDal.UpdateOrderDetailsAsync(orderDetails);
        }

        public async Task ConfirmOrder(int cartId,int userId)
        {
            Cart c = await _cartDal.GetCartByUserIdAsync(userId);
            Order no = new()
            { 
                OrderDate = DateTime.Now,
                Total = c.Total,
                UserID = userId,

            };
            Order newOrder =await _orderDal.AddOrderAsync(no);

            List<Draft> drafts = new();
            drafts = await _draftDal.GetAsync(cartId);
            foreach (var draft in drafts)
            {
                for (int i = 0; i < draft.Quentity; i++)
                {
                    OrderDetails od = new OrderDetails()
                    {
                        OrderId = newOrder.Id,
                        Quentity = 1,
                        GiftId = draft.GiftId,
                    };

                    await _orderDetailsDal.AddOrderDetailsAsync(od);
                    await _purchasesDal.AddPurchaseAsync(od.GiftId);
                }

                await _draftDal.DeleteByIdAsync(draft.Id);
            }
           await _cartDal.ResetCartTotalAsync(cartId);



        }
    }

 }
