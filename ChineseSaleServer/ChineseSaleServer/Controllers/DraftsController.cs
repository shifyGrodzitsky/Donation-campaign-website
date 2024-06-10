using ChineseSaleServer.BL;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChineseSaleServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DraftsController : ControllerBase
    {
        private readonly IDraftService _draftService;

        public DraftsController(IDraftService draftService)
        {
            this._draftService = draftService ?? throw new ArgumentNullException(nameof(draftService));
        }
        // GET: api/<DraftController>
        [HttpGet("{cartId}")]
        public async Task<ActionResult<List<Draft>>> Get(int cartId)
        {
            return await _draftService.Get(cartId);
        }

        // GET api/<DraftController>/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Draft>> GetDraftById(int id)
        //{
        //    return await _draftService.GetById(id);
        //}

        // POST api/<DraftController>
        [HttpPost]
        public async Task Post([FromBody] Draft draft)
        {
            await this._draftService.AddDraft(draft);
        }
        
        [HttpPost("DecreaseDraftQuentity")]
        public async Task DecreaseDraftQuentity([FromBody]int draftId)
        {
            await this._draftService.DecreaseDraftQuentity(draftId);
        }
        [HttpPost("UpdateDraftQuentity")]
        public async Task UpdateDraftQuentity([FromBody]int draftId)
        {
            await this._draftService.UpdateDraftQuentity(draftId);
        }

        // PUT api/<DraftController>/5
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] Draft draft)
        {
            return await _draftService.UpdateDraft(draft);
        }

        // DELETE api/<DraftController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            return await _draftService.DeleteById(id);
        }
    }
}
