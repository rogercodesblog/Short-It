using Microsoft.AspNetCore.Mvc;
using Short_It.DTOs.Link;
using Short_It.Services.LinkService;

namespace Short_It.Controllers
{
    [Route("[controller]")]
    public class LinkController : Controller
    {
        private readonly ILinkService _linkService;
        public LinkController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(LinkDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LinkDTO>> GetLinkByShortUrl(string shortUrl)
        {

            if (string.IsNullOrEmpty(shortUrl))
            {
                return BadRequest("The requested Url cannot be empty");
            }

            var _link = await _linkService.GetLinkByShortUrlAsync(shortUrl);

            if (_link.Success == false)
            {
                ModelState.AddModelError("", _link.Message);
                return StatusCode(500, ModelState);
            }

            return Ok(_link);

        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(LinkDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LinkDTO>> CreateShortLink([FromBody]CreateLinkDTO createLinkDTO)
        {

            if(createLinkDTO == null)
            {
                return BadRequest(ModelState);
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

        [HttpDelete("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteLink(LinkDTO linkDTO)
        {

            if(linkDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _deletedLinkResult = await _linkService.DeleteLinkAsync(linkDTO);

            if(_deletedLinkResult.Success == false)
            {
                ModelState.AddModelError("", _deletedLinkResult.Message);
                return StatusCode(_deletedLinkResult.Message == "The link was not found" ? 404 : 500, ModelState);
            }

            return NoContent();

        }

    }
}
