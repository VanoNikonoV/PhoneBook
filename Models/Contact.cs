using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Contact:IContact
    {
        /// <summary>
        /// Идентификатор контакта
        /// </summary>
        [Display(Name = "№")]
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
        [Required(ErrorMessage = "Не указано отчество")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия")]
        [RegularExpression(@"^[а-яА-Я''-'\s]{1,30}$")]
        [Required(ErrorMessage = "Не указано фамилия")]
        public string LastName { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Display(Name = "Номер телефона")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Не заполенено поле номер телефона ")]
        public string Telefon { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [Display(Name = "Адрес")]
        [Required(ErrorMessage = "Не заполенено поле адрес ")]
        public string Address { get; set; }
        /// <summary>
        /// Описание 
        /// </summary>
        [Display(Name = "Описание")]
        [StringLength(300)]
        public string? Description { get; set; }
  
    }
}
