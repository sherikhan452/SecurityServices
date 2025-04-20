using Microsoft.AspNetCore.Mvc;
using SecurityServices.Models;
using SecurityServices.ViewModels;

namespace SecurityServices.Controllers
{
    public class AccountController : Controller
    {
        private static List<User> Users = new List<User>();

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (Users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("", "Username already exists.");
                return View(model);
            }

            Users.Add(new User { Username = model.Username, Password = model.Password });
            return RedirectToAction("Login");
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(model);
            }


         
            HttpContext.Session.SetString("username", user.Username);
            if (user.Username == "admin")
            {
                return RedirectToAction("Index", "Dashboard");
            }

            HttpContext.Session.SetString("username", user.Username);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Login");
        }
    }
}
