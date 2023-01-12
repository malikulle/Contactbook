using Contact.Framework.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
