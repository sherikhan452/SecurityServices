using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityServices.Data;
using SecurityServices.Models;

namespace SecurityServices.Controllers
{
    public class ImageControllers : Controller
    {
        private readonly ApplicationContext _context;

        public ImageControllers(ApplicationContext context)
        {
            _context = context;
        }

        // Create (GET): Display form to create a new service
        public IActionResult Create()
        {
            return View();
        }

        // Create (POST): Handle the form submission for creating a service
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile imageFile, string name, string description)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);

                    var service = new ImageModelAgain
                    {
                        Name = name,
                        Description = description,
                        FileName = imageFile.FileName,
                        ContentType = imageFile.ContentType,
                        Data = memoryStream.ToArray()
                    };

                    _context.Add(service);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        // Index (GET): List all services
        public async Task<IActionResult> Index()
        {
            var services = await _context.Imagess.ToListAsync();
            return View(services);
        }

        // Details (GET): View details of a specific service
        public async Task<IActionResult> Details(int id)
        {
            var service = await _context.Imagess.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // Edit (GET): Display form to edit a service
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _context.Imagess.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // Edit (POST): Handle the form submission for editing a service
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile imageFile, string name, string description)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var service = await _context.Imagess.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);

                    service.Name = name;
                   
                    service.Description = description;
                    service.FileName = imageFile.FileName;
                    service.ContentType = imageFile.ContentType;
                    service.Data = memoryStream.ToArray();

                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                service.Name = name;
                service.Description = description;
                _context.Update(service);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Delete (GET): Display confirmation page to delete a service
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _context.Imagess.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // Delete (POST): Handle the deletion of a service
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Imagess.FindAsync(id);
            if (service != null)
            {
                _context.Imagess.Remove(service);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Get image as a file (for displaying the service image)
        public IActionResult GetServiceImage(int id)
        {
            var service = _context.Imagess.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return File(service.Data,service.ContentType);
        }
    }
}

