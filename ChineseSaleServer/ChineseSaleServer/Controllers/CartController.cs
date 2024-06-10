using ChineseSaleServer.BL;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChineseSaleServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
                this._cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        }
        // GET: api/<CartController>
        [HttpGet]
        public async Task<ActionResult<Cart>> Get()
        {
            User middlewareUser = ControllerContext.HttpContext.Items["User"] as User;
            Console.WriteLine($"User ID: {middlewareUser?.Id}");
            return await _cartService.GetCartByUserId(middlewareUser.Id);
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
