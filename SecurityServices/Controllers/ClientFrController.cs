using Microsoft.AspNetCore.Mvc;

namespace SecurityServices.Controllers
{
    public class ClientFrController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
