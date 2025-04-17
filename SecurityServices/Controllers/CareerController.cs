using Microsoft.AspNetCore.Mvc;

namespace SecurityServices.Controllers
{
    public class CareerController : Controller
    {
        public IActionResult Index()
        {     
            return View();
        }
    }
}
