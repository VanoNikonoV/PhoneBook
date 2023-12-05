using Microsoft.AspNetCore.Mvc;
using PhoneBook.Interfaces;
using PhoneBook.Models;
using System.Net;

namespace PhoneBook.Controllers
{
    [Controller]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationData _context;
        private readonly IRequestLogin _login;

        public AuthenticationController(IAuthenticationData context, IRequestLogin login)
        {
            _context = context;
            _login = login;
        }

        public IActionResult Login() { return View(); }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(RequestLogin request)
        {
            HttpStatusCode httpStatusCode = await _context.Login(request);

            if (httpStatusCode == HttpStatusCode.OK)
            {
                _login.IsToken = true;

                _login.Email = request.Email;
            }

            if (httpStatusCode == HttpStatusCode.NotFound) { return Problem("Нет клиента"); }

            else return Redirect(@"\Contacts\Index");
        }

        public IActionResult Register() { return View(); }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(User user)
        {
           if (user.Password == user.ConfirmPassword) 
           {
                HttpStatusCode httpStatusCode = await _context.Register(user);

                if (httpStatusCode == HttpStatusCode.OK)
                {
                    return Redirect("~/Authentication/Login");
                }
                else { return Problem(httpStatusCode.ToString()); }
           }
           return View();
        }

        public IActionResult Logout() 
        {
            _login.IsToken = false;

            AccessForToken.Token = string.Empty;

            return Redirect(@"\Contacts\Index");
        }
    }
}
