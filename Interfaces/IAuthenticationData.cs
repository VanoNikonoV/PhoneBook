using PhoneBook.Models;
using System.Net;

namespace PhoneBook.Interfaces
{
    public interface IAuthenticationData
    {
        Task<HttpStatusCode> Login(RequestLogin request);

        Task<HttpStatusCode> Register(User user);
    }
}
