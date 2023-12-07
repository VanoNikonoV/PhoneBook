using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Contact : IContact
    {
        /// <summary>
        /// Идентификатор контакта
        /// </summary>
        [Display(Name = "№")]
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Имя")]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Отчество")]
        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия")]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Display(Name = "Номер телефона")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [JsonProperty("telefon")]
        public string Telefon { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [Display(Name = "Адрес")]
        [JsonProperty("address")]
        public string Address { get; set; }
        /// <summary>
        /// Описание 
        /// </summary>
        [Display(Name = "Описание")]
        [JsonProperty("description")]
        public string? Description { get; set; }
  
    }
}
