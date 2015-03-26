using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WPFAddressBook.Entities;

namespace WPFAddressBook
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext() : base("AddressBookConnection")
        {
            
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}