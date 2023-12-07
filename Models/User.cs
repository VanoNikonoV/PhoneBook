using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class User
    {
        [Display(Name = "Имя")]
        [RegularExpression(@"^[а-яА-Я''-'\s]{1,30}$", ErrorMessage = "Имя не корретно")]
        [Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Фамилия")]
        [RegularExpression(@"^[а-яА-Я''-'\s]{1,30}$")]
        [Required(ErrorMessage = "Не указано фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "e-mail")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Не заполенено поле e-mail")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не заполенено поле пароль")]
        public string Password{ get; set; } = string.Empty;

        [Display(Name = "Повоторите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Required(ErrorMessage = "Не заполенено поле повоторите пароль")]
        public string ConfirmPassword {  get; set; } = string.Empty;
    }
}
