using PhoneBook.Models;

namespace PhoneBook.Interfaces
{
    public interface IAuthenticationData
    {
        Task<bool> Login(RequestLogin request);

        Task Register(User user);
    }
}
