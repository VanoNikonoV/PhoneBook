using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;

namespace PhoneBook.Models
{
    /// <summary>
    /// Класс для наполнения данными БД
    /// </summary>
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PhoneBookContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PhoneBookContext>>()))
            {
                // Look for any movies.
                if (context.Contact.Any())
                {
                    return;   // DB has been seeded
                }
                context.Contact.AddRange(
                    new Contact
                    {
                        FirstName = "Иван",
                        MiddleName = "Иваныч",
                        LastName = "Иванов",
                        Telefon = "79827135444",
                        Address = "Москва",
                        Description = "Description",

                    },
                    new Contact
                    {
                        FirstName = "Иван",
                        MiddleName = "Андреевич",
                        LastName = "Путин",
                        Telefon = "79827855040",
                        Address = "Свердловск",
                        Description = "Description",

                    },
                    new Contact
                    {
                        FirstName = "Костя",
                        MiddleName = "Владимирович",
                        LastName = "Хмель",
                        Telefon = "79826665040",
                        Address = "Тюмень",
                        Description = "есть",
                    },
                    new Contact
                    {
                        FirstName = "Петр",
                        MiddleName = "Иваныч",
                        LastName = "Степанов",
                        Telefon = "79827135040",
                        Address = "Пенза",
                        Description = "нет",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
