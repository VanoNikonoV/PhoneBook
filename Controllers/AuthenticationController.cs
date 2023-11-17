using Microsoft.AspNetCore.Mvc;
using PhoneBook.Interfaces;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationData _context;

        public AuthenticationController(IAuthenticationData context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User request)
        {
            string token = _context.Login(request).Result;

            return Redirect(@"\Contacts\Index");
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
