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

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Зашиврованный пароль пользователя
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
    }
}
