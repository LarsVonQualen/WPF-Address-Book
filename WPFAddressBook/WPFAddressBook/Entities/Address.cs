namespace WPFAddressBook.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressString { get; set; }
        public int PersonId { get; set; }

        public Address()
        {
            
        }

        public Address(int id, string addressString, int personId)
        {
            Id = id;
            AddressString = addressString;
            PersonId = personId;
        }

        public Address(string addressString, int personId)
        {
            AddressString = addressString;
            PersonId = personId;
        }
    }
}