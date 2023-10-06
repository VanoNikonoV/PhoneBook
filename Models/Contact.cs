using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Contact
    {
        /// <summary>
        /// Идентификатор контакта
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Имя")]
        [RegularExpression(@"^[а-яА-Я''-'\s]{1,30}$", ErrorMessage ="Имя не корретно")]
        [Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Отчество")]
        [RegularExpression(@"^[а-яА-Я''-'\s]{1,30}$")]
        [Required]
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия")]
        [RegularExpression(@"^[а-яА-Я''-'\s]{1,30}$")]
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Display(Name = "Номер телефона")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string Telefon { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        /// <summary>
        /// Описание 
        /// </summary>
        [Display(Name = "Описание")]
        public string Description { get; set; }
  
    }
}
