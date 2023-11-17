using Microsoft.AspNetCore.Mvc;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User request)
        {
            return View(request);
        }

        public IActionResult Register() 
        {
            return View();
        }

        public IActionResult Logout() 
        {
            return View();
        }
    }
}
