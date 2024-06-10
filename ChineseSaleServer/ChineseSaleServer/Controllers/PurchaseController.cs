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
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchasesService _purchasesService;

        public PurchaseController(IPurchasesService purchasesService)
        {
            this._purchasesService = purchasesService ?? throw new ArgumentNullException(nameof(purchasesService));
        }
        // GET: api/<PurchaseController>
        [Authorize(Roles = "admin")]
        [HttpGet("SortByExpensiveGift")]
        public async Task<ActionResult<List<Gift>>> GetSortByExpensiveGift()
        {
            return await _purchasesService.SortByExpensiveGift();
        }
        [Authorize(Roles = "admin")]
        [HttpGet("SortByNumOfPurchases")]
        public async Task<ActionResult<List<Gift>>> GetSortByNumOfPurchases()
        {
            return await _purchasesService.SortByNumOfPurchases();
        }

        // GET api/<PurchaseController>/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<User>>> Get(int id)
        {
            return await _purchasesService.PurchasesDetails(id);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("GetPurchasesOrderDetailes/{giftId}")]
        public async Task<ActionResult<List<OrderDetails>>> GetPurchasesOrderDetailes(int giftId)
        {
            return await _purchasesService.PurchasesOrderDetails(giftId);
        }



    }
}
