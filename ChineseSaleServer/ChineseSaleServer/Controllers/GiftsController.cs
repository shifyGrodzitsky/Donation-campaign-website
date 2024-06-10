using System.Runtime.CompilerServices;
using ChineseSaleServer.BL;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChineseSaleServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftsController : ControllerBase
    {
        private readonly IGiftService _giftService;

        public GiftsController(IGiftService giftService)
        {
            this._giftService = giftService?? throw new ArgumentNullException(nameof(giftService));
        }
        // GET: api/<GiftsController>
        [HttpGet]
        public async Task <ActionResult<List<Gift>>> Get()
        {
            return await _giftService.GetGifts();   
        }

        // GET api/<GiftsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gift>> Get(int id)
        {
            return await _giftService.GetGiftById(id); 
        }

        // POST api/<GiftsController>
        [HttpPost]
        public async Task Post([FromBody] Gift gift)   
        {
             await this._giftService.AddGift(gift);
        }

        // PUT api/<GiftsController>/5
        [HttpPut]
        public async Task<ActionResult<bool>> Put( [FromBody] Gift gift)
        {
        return await _giftService.UpdateGift(gift);
        }

        // DELETE api/<GiftsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            return await _giftService.DeleteGiftById(id);
        }

        [HttpGet("search/{type}/{value}")]
        public async Task<ActionResult<List<Gift>>> GetByFilter(string type, string value)
        {
            return await _giftService.GetGiftByFilter(type, value);
        }


        //user
        [HttpGet("sortPrice")]
        public async Task<ActionResult<List<Gift>>> GetSortByPrice()
        {
            return await _giftService.SortGiftsByPrice();
        }
        [HttpGet("sortPriceDesc")]
        public async Task<ActionResult<List<Gift>>> GetSortByPriceDesc()
        {
            return await _giftService.SortGiftsByPriceDesc();
        }
        [HttpGet("sortByCategory")]
        public async Task<ActionResult<List<Gift>>> GetSortByCategory(string category)
        {
            return await _giftService.SortGiftsByCategory(category);
        }
    }
}
