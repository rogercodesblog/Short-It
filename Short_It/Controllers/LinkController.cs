using Microsoft.AspNetCore.Mvc;
using Short_It.DTOs.Link;
using Short_It.Services.LinkService;

namespace Short_It.Controllers
{
    public class LinkController : Controller
    {
        private readonly ILinkService _linkService;
        public LinkController(ILinkService linkService)
        {
            _linkService = linkService;
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
                return StatusCode(500, ModelState);
            }

            return Ok(_newLink);

        }



    }
}
