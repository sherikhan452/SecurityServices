using Microsoft.AspNetCore.Mvc;

namespace SecurityServices.Controllers
{
    public class Aboutus : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
