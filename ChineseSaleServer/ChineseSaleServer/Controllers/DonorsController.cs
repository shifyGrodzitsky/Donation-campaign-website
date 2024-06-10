using ChineseSaleServer.BL;
using ChineseSaleServer.DAL;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChineseSaleServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
   //
    public class DonorsController : ControllerBase
    {
        private readonly IDonorService _donorService;
      

        public DonorsController(IDonorService donorService)
        {
            this._donorService = donorService ?? throw new ArgumentNullException(nameof(donorService));
        }
        // GET: api/<DonorsController>
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<List<Donor>>> Get()
        {
            return await _donorService.GetDonors();
        }

        // GET api/<DonorsController>/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Donor>> Get(int id)
        {
            return await _donorService.GetDonorById(id);
        }

        // POST api/<DonorsController>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task Post([FromBody] Donor donor)
        {
            await _donorService.AddDonor(donor);
        }

        // PUT api/<DonorsController>/5
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] Donor donor)
        {
            return await _donorService.UpdateDonor(donor);
        }

        // DELETE api/<DonorsController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            return await _donorService.DeleteDonorById(id);
        }

        // get gifts for donod
        [Authorize(Roles = "admin")]
        [HttpGet("list_of_gift/{id}")]
        public async Task<ActionResult<List<Gift>>> GetGiftByDonorId(int id)
        {
            return await _donorService.GetGiftsByDonor(id);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("search/{type}/{value}")]
        public async Task<ActionResult<Donor>> GetByFilter(string type,string value)
        {
            return await _donorService.GetDonorByFilter(type,value);
        }

    }
}
