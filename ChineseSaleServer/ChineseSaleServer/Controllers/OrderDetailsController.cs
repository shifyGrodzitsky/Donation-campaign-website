using ChineseSaleServer.BL;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChineseSaleServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    { 
   private readonly IOrderDetailsService _orderDetailsService;

    public OrderDetailsController(IOrderDetailsService orderDetailsService)
    {
        this._orderDetailsService = orderDetailsService ?? throw new ArgumentNullException(nameof(orderDetailsService));
    }
    // GET: api/<OrderDetailsController>
    [HttpGet]
    public async Task<ActionResult<List<OrderDetails>>> Get()
    {
        return await _orderDetailsService.GetOrderDetails();
    }

    // GET api/<OrderDetailsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetails>> Get(int id)
    {
        return await _orderDetailsService.GetOrderDetailsById(id);
    }

    // POST api/<OrderDetailsController>
    [HttpPost]
    public async Task Post([FromBody]int cartId)
    {
            User middlewareUser = ControllerContext.HttpContext.Items["User"] as User;
            await this._orderDetailsService.ConfirmOrder(cartId,middlewareUser.Id);
    }

    // PUT api/<OrderDetailsController>/5
    [HttpPut]
    public async Task<ActionResult<bool>> Put([FromBody] OrderDetails orderDetails)
    {
        return await _orderDetailsService.UpdateOrderDetails(orderDetails);
    }

    // DELETE api/<OrderDetailsController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(int id)
    {
        return await _orderDetailsService.DeleteOrderDetailsById(id);
    }

}
}
