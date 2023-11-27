using PhoneBook.Interfaces;

namespace PhoneBook.Models
{
   
    public class RequestLogin: IRequestLogin
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool IsToken { get; set; } = false;
    }
}
