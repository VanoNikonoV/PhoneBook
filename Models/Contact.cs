﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Contact:IContact
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
        [RegularExpression(@"^[а-яА-Я''-'\s]{1,30}$", ErrorMessage ="Имя не корретно")]
        [Required(ErrorMessage = "Не указано имя")]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Отчество")]
        [RegularExpression(@"^[а-яА-Я''-'\s]{1,30}$")]
        [Required(ErrorMessage = "Не указано отчество")]
        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия")]
        [RegularExpression(@"^[а-яА-Я''-'\s]{1,30}$")]
        [Required(ErrorMessage = "Не указано фамилия")]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Display(Name = "Номер телефона")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Не заполенено поле номер телефона ")]
        [JsonProperty("telefon")]
        public string Telefon { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [Display(Name = "Адрес")]
        [Required(ErrorMessage = "Не заполенено поле адрес ")]
        [JsonProperty("address")]
        public string Address { get; set; }
        /// <summary>
        /// Описание 
        /// </summary>
        [Display(Name = "Описание")]
        [StringLength(300)]
        [JsonProperty("description")]
        public string? Description { get; set; }
  
    }
}
