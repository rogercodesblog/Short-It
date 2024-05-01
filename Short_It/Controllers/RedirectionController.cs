using Microsoft.AspNetCore.Mvc;

namespace Short_It.Controllers
{
    public class RedirectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
