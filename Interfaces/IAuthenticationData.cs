using PhoneBook.Models;

namespace PhoneBook.Interfaces
{
    public interface IAuthenticationData
    {
        Task<string> Login(RequestLogin request);
    }
}
