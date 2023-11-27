using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using PhoneBook.Interfaces;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    [Controller]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationData _context;
        private readonly IRequestLogin _login;

        public string Token { get; set; } = string.Empty;

        public AuthenticationController(IAuthenticationData context, IRequestLogin login)
        {
            _context = context;
            _login = login;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(RequestLogin request)
        {
            var token = await _context.Login(request);

            HttpContext.Session.SetString("JWTtoken", token);

            _login.Email = request.Email;
            _login.IsToken = true;

            return Redirect(@"\Contacts\Index");
        }

        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
           if (user.Password == user.ConfirmPassword) 
           {
                _context.Register(user);

                return Redirect(@"\Login");

           }
           return View();
        }

        public IActionResult Logout() 
        {
            HttpContext.Session?.Clear();

            return Redirect(@"\Contacts\Index");
        }
    }
}
