using FluentValidation;

namespace PhoneBook.Models
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator() 
        {
            RuleFor(contact => contact.FirstName)
                      .NotEmpty().WithMessage("Имя не заполнено")
                      .Must(contact => contact.All(Char.IsLetter)).WithMessage("Имя должно собержать только буквы")
                      .Must(StartsWithUpper).WithMessage("Имя должно начинаться с заглавной буквы");

            RuleFor(contact => contact.MiddleName)
                .NotEmpty().WithMessage("Отчество не заполнено")
                .Must(contact => contact.All(Char.IsLetter)).WithMessage("Отчество  должно собержать только буквы")
                .Must(StartsWithUpper).WithMessage("Отчество должно начинаться с заглавной буквы");

            RuleFor(contact => contact.LastName)
                .NotEmpty().WithMessage("Фамилия не заполнена")
                .Must(contact => contact.All(Char.IsLetter)).WithMessage("Фамилия должно собержать только буквы")
                .Must(StartsWithUpper).WithMessage("Фамилия должно начинаться с заглавной буквы");

            RuleFor(t => t.Telefon)
                .NotEmpty().WithMessage("Нужно указать значение")
                .Must(t => t.StartsWith("+79")).WithMessage("Номер долже начинаться +79...")
                .Length(12).WithMessage("Длина должна быть {MinLength}. Текущая длина: {TotalLength}")
                .Must(TelefonIsDigit).WithMessage("Номер долже содержать только цифры");
        }

        /// <summary>
        /// Определяет является ли первым символом 
        /// текущего экземпляра строки символ верхнего регистра
        /// </summary>
        /// <param name="str">Проеряемая строка</param>
        /// <returns>true - если превая буква заглавная
        /// false - если превая буква незаглавная </returns>
        public bool StartsWithUpper(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            char ch = str[0];
            return char.IsUpper(ch);
        }

        /// <summary>
        /// Показывает, относится ли указанные символы Юникода к категории десятичных цифр, 
        /// если value - не равно hull или string.Empty
        /// </summary>
        /// <param name="value">Номер телефона</param>
        /// <returns>true - если телефон состоит только из цифр,
        /// в противном случае - false</returns>
        private bool TelefonIsDigit(string value)
        {
            if (string.IsNullOrEmpty(value)) return true;

            return value.Skip(1).All(Char.IsDigit);
        }
    }
}
