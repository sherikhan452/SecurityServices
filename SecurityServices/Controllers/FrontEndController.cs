using Microsoft.AspNetCore.Mvc;

namespace SecurityServices.Controllers
{
    public class FrontEndController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
