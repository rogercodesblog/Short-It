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

        //Get Link Stats (LinkStats(DTO)

        //Create Link (CreateLinkDTO)

    }
}
