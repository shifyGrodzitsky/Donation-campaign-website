using ChineseSaleServer.BL;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChineseSaleServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            this._orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }


        //// GET: api/<OrdersController>
        //[HttpGet]
        //public async Task<ActionResult<List<Order>>> Get()
        //{
        //    return await _orderService.GetOrders();
        //}

        // GET api/<OrdersController>/5
        [HttpGet]
        public async Task<ActionResult<Order>> Get()
        {
            User middlewareUser = ControllerContext.HttpContext.Items["User"] as User;
            return await _orderService.GetOrderById(middlewareUser.Id);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] Order order)
        {
           return await this._orderService.AddOrder(order);
        }

        // PUT api/<OrdersController>/5
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] Order order)
        {
            return await _orderService.UpdateOrder(order);
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            return await _orderService.DeleteOrderById(id);
        }
    }
}
