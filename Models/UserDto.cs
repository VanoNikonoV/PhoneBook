using System.Security.Claims;

namespace PhoneBook.Models
{
    public class UserDto
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
