using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using NuGet.Common;
using PhoneBook.Interfaces;
using PhoneBook.Models;

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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(RequestLogin request)
        {
            string token = await _context.Login(request);

            HttpContext.Session.SetString("JWTtoken", token);

            _login.Email = request.Email;
            _login.IsToken = true;

            if (!_login.IsToken) { return Problem("Нет клиента"); } //нужно сообщение об ощибке на 

            else return Redirect(@"\Contacts\Index");
        }

        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(User user)
        {
           if (user.Password == user.ConfirmPassword) 
           {
                await _context.Register(user);

                return Redirect("~/Authentication/Login"); 

           }
           return View();
        }

        public IActionResult Logout() 
        {
            _login.IsToken = false;

            AccessForToken.Token= string.Empty;

            return Redirect(@"\Contacts\Index");
        }
    }
}
