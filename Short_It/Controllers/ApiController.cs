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
        [HttpGet]
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
        [HttpGet]
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
        [HttpPost]
        public async Task<ActionResult<LinkDTO>> CreateShortLink([FromBody] CreateLinkDTO createLinkDTO)
        {
            if (createLinkDTO == null)
            {
                return BadRequest("The requested Url cannot be empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _newLink = await _linkService.AddLinkAsync(createLinkDTO);

            if (_newLink.Success == false)
            {
                ModelState.AddModelError("", _newLink.Message);
                return BadRequest(ModelState);
            }

            if (_newLink.Success == false && _newLink.IsInteralError == true)
            {
                ModelState.AddModelError("", _newLink.Message);
                return StatusCode(500, ModelState);
            }

            return Ok(_newLink);
        }

    }
}
