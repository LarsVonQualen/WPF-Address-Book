using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace WPFAddressBook.DAL
{
    public class PersonManager
    {
        public async Task<Person> Create(Person person)
        {
            using (var db = new AddressBookContext())
            {
                db.Persons.Add(person);
                await db.SaveChangesAsync();

                return person;
            }
        }

        public async Task<Person> Update(Person person)
        {
            using (var db = new AddressBookContext())
            {
                db.Persons.Attach(person);

                var updatedPerson = db.Entry(person);
                updatedPerson.State = EntityState.Modified;
                
                await db.SaveChangesAsync();

                return updatedPerson.Entity;
            }
        }

        public async Task<IEnumerable<Person>> Read()
        {
            using (var db = new AddressBookContext())
            {
                var persons = await db.Persons.Include(p => p.Addresses).ToListAsync();
                return persons;
            }
        }

        public async Task<Person> Read(int id)
        {
            using (var db = new AddressBookContext())
            {
                var person = await db.Persons.Include(p => p.Addresses).SingleOrDefaultAsync(p => p.Id == id);

                return person;
            }
        }

        public async Task Delete(Person person)
        {
            using (var db = new AddressBookContext())
            {
                db.Persons.Attach(person);
                db.Persons.Remove(person);
                await db.SaveChangesAsync();
            }
        }
    }
}