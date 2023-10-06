using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

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
