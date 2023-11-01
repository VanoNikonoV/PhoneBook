using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public interface IContact
    {
        /// <summary>
        /// Идентификатор контакта
        /// </summary>
        [Display(Name = "№")]
        [JsonProperty("id")]
        int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Имя")]
        [JsonProperty("firstName")]
        string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Отчество")]
        [JsonProperty("middleName")]
        string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия")]
        [JsonProperty("lastName")]
        string LastName { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Display(Name = "Номер телефона")]
        [JsonProperty("telefon")]
        string Telefon { get; set; }

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
        string? Description { get; set; }
    }
}
