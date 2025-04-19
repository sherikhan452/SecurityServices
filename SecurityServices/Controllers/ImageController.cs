using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityServices.Data;
using SecurityServices.Models;

namespace SecurityServices.Controllers
{
    public class ImageController : Controller
    {
        
            private readonly ApplicationContext _context;

            public ImageController(ApplicationContext context)
            {
                _context = context;
            }

            // Create (GET): Display image upload form
            public IActionResult Create()
            {
                return View();
            }

            // Create (POST): Handle image upload and save to the database
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(IFormFile imageFile)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(memoryStream);

                        var image = new ImageModel
                        {
                            FileName = imageFile.FileName,
                            ContentType = imageFile.ContentType,
                            Data = memoryStream.ToArray()
                        };

                        _context.Add(image);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }

                return View();
            }

            // Index (GET): Display all images
            public async Task<IActionResult> Index()
            {
                var images = await _context.Images.ToListAsync();
                return View(images);
            }

            // Details (GET): View details of a single image
            public async Task<IActionResult> Details(int id)
            {
                var image = await _context.Images.FindAsync(id);
                if (image == null)
                {
                    return NotFound();
                }

                return View(image);
            }

            // Edit (GET): Display the edit form
            public async Task<IActionResult> Edit(int id)
            {
                var image = await _context.Images.FindAsync(id);
                if (image == null)
                {
                    return NotFound();
                }

                return View(image);
            }

            // Edit (POST): Save updated image details
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, IFormFile imageFile)
            {
                if (id == 0)
                {
                    return NotFound();
                }

                var image = await _context.Images.FindAsync(id);
                if (image == null)
                {
                    return NotFound();
                }

                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(memoryStream);

                        image.FileName = imageFile.FileName;
                        image.ContentType = imageFile.ContentType;
                        image.Data = memoryStream.ToArray();

                        _context.Update(image);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            // Delete (GET): Display delete confirmation page
            public async Task<IActionResult> Delete(int id)
            {
                var image = await _context.Images.FindAsync(id);
                if (image == null)
                {
                    return NotFound();
                }

                return View(image);
            }

            // Delete (POST): Delete image from database
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var image = await _context.Images.FindAsync(id);
                if (image != null)
                {
                    _context.Images.Remove(image);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            // Get image as a file (to display in the browser)
            public IActionResult GetImage(int id)
            {
                var image = _context.Images.FirstOrDefault(i => i.Id == id);
                if (image == null)
                {
                    return NotFound();
                }

                return File(image.Data, image.ContentType);
            }
        }

    }
