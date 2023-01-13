using Contact.Framework.Entity;
using Microsoft.EntityFrameworkCore;

namespace Contact.Framework.Context
{
    public class ContactDbContext : DbContext
    {

        public ContactDbContext(DbContextOptions<ContactDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<PersonContact> PersonContacts { get; set; }
    }
}
