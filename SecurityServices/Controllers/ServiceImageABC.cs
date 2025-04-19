using Microsoft.AspNetCore.Mvc;
using SecurityServices.Data;
using SecurityServices.Models;

namespace SecurityServices.Controllers
{
    public class ServiceImageABC : Controller
    {
        private readonly ApplicationContext _context;

        public ServiceImageABC(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Get()
        {
            return View();
        }
        public IActionResult GetImage(int id)
        {
            var imageData = _context.ServiceImages.FirstOrDefault(x => x.ServiceId == id);

            if (imageData == null || imageData.Image == null)
                return NotFound();

            return File(imageData.Image, "image/jpeg"); // or "image/png"
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(ServiceImage model)
        {
            if (model.File != null && model.File.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", model.File.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                return Ok("File uploaded successfully");
            }

            return BadRequest("File not selected");
        }

    }
}
