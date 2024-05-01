using Microsoft.AspNetCore.Mvc;
using Short_It.Services;
using Short_It.Services.RedirectionService;
using System.Xml.Linq;

namespace Short_It.Controllers
{
    public class RedirectionController : Controller
    {
        private readonly IRedirectionService _redirectionService;
        public RedirectionController(IRedirectionService redirectionService)
        {
            _redirectionService = redirectionService;
        }

        [HttpGet("to/{LinkProvided?}")]
        public async Task<ActionResult> RedirectShortUrl(string LinkProvided)
        {
            //return Content("OK");

            if (string.IsNullOrEmpty(LinkProvided))
            {
                return RedirectToAction("Index", "Home");
            }

            var _ServiceResponse = await _redirectionService.GetLinkToRedirectByShortUrlAsync(LinkProvided);

            //People ask why im using "==false" instead of the "!" operator at the beggining
            //In these cases, this is more readable and achieves the same functionality
            if (_ServiceResponse.Success == false)
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(_ServiceResponse.Data);

        }
    }
}
