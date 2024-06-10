using System.Data;
using ChineseSaleServer.BL;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChineseSaleServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private readonly IRandomService _randomService;

        public RandomController(IRandomService randomService)
        {
            this._randomService = randomService ?? throw new ArgumentNullException(nameof(randomService));
        }
        // GET: api/<RandomController>
        [HttpGet]
        public async Task<ActionResult<List<RandomClass>>> GetRandom()
        {
            return await _randomService.Get();
        }

        // GET api/<RandomController>/5
        [Authorize(Roles = "admin")]
        [HttpGet("{giftId}")]
        public async Task<ActionResult<User>> Get(int giftId)
        {
            return await _randomService.GiftRandom(giftId);
        }

        // POST api/<RandomController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RandomController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RandomController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
