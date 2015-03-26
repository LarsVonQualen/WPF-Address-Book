using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WPFAddressBook.Entities;

namespace WPFAddressBook.DAL
{
    public class AddressManager
    {
        public async Task<Address> Create(Address address)
        {
            using (var db = new AddressBookContext())
            {
                db.Addresses.Add(address);
                await db.SaveChangesAsync();

                return address;
            }
        }

        public async Task<Address> Update(Address address)
        {
            using (var db = new AddressBookContext())
            {
                db.Addresses.Attach(address);

                var updatedAddress = db.Entry(address);
                updatedAddress.State = EntityState.Modified;

                await db.SaveChangesAsync();

                return updatedAddress.Entity;
            }
        }

        public async Task<IEnumerable<Address>> Read()
        {
            using (var db = new AddressBookContext())
            {
                var address = await db.Addresses.ToListAsync();

                return address;
            }
        }

        public async Task<IEnumerable<Address>> Read(Person person)
        {
            using (var db = new AddressBookContext())
            {
                var address = await db.Addresses.Where(a => a.PersonId == person.Id).ToListAsync();

                return address;
            }
        }

        public async Task<Address> Read(int id)
        {
            using (var db = new AddressBookContext())
            {
                var address = await db.Addresses.FindAsync(id);

                return address;
            }
        }

        public async Task Delete(Address address)
        {
            using (var db = new AddressBookContext())
            {
                db.Addresses.Attach(address);
                db.Addresses.Remove(address);
                await db.SaveChangesAsync();
            }
        }
    }
}