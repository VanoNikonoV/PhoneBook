namespace PhoneBook.Models
{
    public class NullContact : IContact
    {
        private NullContact() 
        {
            this.Id = 0;
            this.FirstName = "без имени";
            this.MiddleName = "без отчества";
            this.LastName = "без фамилии";
            this.Address = "без адреса";
            this.Telefon = "без телефона";
            this.Description = "без описания";
        }

        static public NullContact Create() { return new NullContact(); }

        /// <summary>
        /// Идентификатор контакта
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Telefon { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Описание 
        /// </summary>
        public string? Description { get; set; }
    }
}
