using ChineseSaleServer.BL;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace ChineseSaleServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [HttpPost("register")]
        public async Task Post([FromBody] User model)
        {
            await this._authenticationService.Register(model);

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginModel model)
        {
            return await _authenticationService.Login(model.Email, model.Password);
        }

    }
}
