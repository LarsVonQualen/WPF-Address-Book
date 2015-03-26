using System.Collections.Generic;
using WPFAddressBook.Entities;

namespace WPFAddressBook
{
    public class Person
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public List<Address> Addresses { get; set; } 

        public Person()
        {
            
        }

        public Person(int id, string first, string last, string phone)
        {
            Id = id;
            Firstname = first;
            Lastname = last;
            Phone = phone;
        }

        public Person(string first, string last, string phone)
        {
            Firstname = first;
            Lastname = last;
            Phone = phone;
        }
    }
}