namespace PhoneBook.Models
{
    public interface IContact
    {
        /// <summary>
        /// Идентификатор контакта
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        string Telefon { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Описание 
        /// </summary>
        string? Description { get; set; }
    }
}
