using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Short_It.DTOs.Link;
using Short_It.Services.LinkService;
using Short_It.Services.RedirectionService;

namespace Short_It.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ILinkService _linkService;
        public ApiController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        //To be Implemented:

        //Get Link by Short Url (LinkDTO)
        public async Task<ActionResult<LinkDTO>> GetShortLink(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest("The requested Url cannot be empty");
            }

            var _link = await _linkService.GetLinkByShortUrlAsync(url);

            if (_link.Success == false)
            {
                ModelState.AddModelError("", _link.Message);
                return StatusCode(500, ModelState);
            }

            return Ok(_link);
        }

        //Get Link Stats (LinkStats(DTO)
        public async Task<ActionResult<LinkStatsDTO>> GetLinkStats(string url)
        {
            if(string.IsNullOrEmpty(url))
            {
                return BadRequest("The requested Url cannot be empty");
            }

            var _link = await _linkService.GetLinkStatsByShortUrlAsync(url);

            if(_link.Success == false)
            {
                ModelState.AddModelError("", _link.Message);
                return StatusCode(500, ModelState);
            }

            return Ok(_link);

        }

        //Create Link (CreateLinkDTO)

    }
}
