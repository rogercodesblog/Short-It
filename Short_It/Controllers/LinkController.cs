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

    }
}
