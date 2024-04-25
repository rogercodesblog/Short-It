using Microsoft.AspNetCore.Mvc;

namespace Short_It.Controllers
{
    public class LinkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
