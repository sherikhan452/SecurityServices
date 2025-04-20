using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityServices.Data;

namespace SecurityServices.Controllers
{
    public class FrontEndController : Controller
    {
        private readonly ApplicationContext _context;

        public FrontEndController(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var services = await _context.Imagess.ToListAsync();
            return View(services);
        }

        public IActionResult GetServiceImage(int id)
        {
            var service = _context.Imagess.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return File(service.Data, service.ContentType);
        }
    }
}
