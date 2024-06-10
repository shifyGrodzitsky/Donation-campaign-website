using ChineseSaleServer.BL;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChineseSaleServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return await _userService.GetUsers();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            return await _userService.GetUserById(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task Post([FromBody] User user)
        {
            await _userService.AddUser(user);
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] User user)
        {
            return await _userService.UpdateUser(user);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            return await _userService.DeleteUserById(id);
        }
    }
}
