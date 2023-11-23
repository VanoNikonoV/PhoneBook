using Microsoft.AspNetCore.Identity;

namespace PhoneBook.Models
{
    public class User
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; }

        public string Password{ get; set; } = string.Empty;

        public string ConfirmPassword {  get; set; } = string.Empty;
    }
}
