using Microsoft.EntityFrameworkCore;

namespace PhoneBook.Data
{
    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext (DbContextOptions<PhoneBookContext> options)
            : base(options)
        {
        }

        public DbSet<PhoneBook.Models.Contact> Contact { get; set; } = default!;
    }
}
