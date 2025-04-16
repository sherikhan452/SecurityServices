using Microsoft.AspNetCore.Mvc;

namespace SecurityServices.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
