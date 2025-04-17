using Microsoft.AspNetCore.Mvc;

namespace SecurityServices.Controllers
{
    public class ServController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
