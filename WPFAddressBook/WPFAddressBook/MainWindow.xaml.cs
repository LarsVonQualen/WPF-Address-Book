using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFAddressBook.DAL;
using WPFAddressBook.Entities;

namespace WPFAddressBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _index = 0;
        private PersonManager _personManager;
        private AddressManager _addressManager;

        public MainWindow()
        {
            InitializeComponent();
            _personManager = new PersonManager();
            _addressManager = new AddressManager();

            FillListAsync();
        }

        private async void FillListAsync()
        {
            var persons = await _personManager.Read();
            persons.ToList().ForEach(person => ContactsList.Items.Add(person));
        }

        private async void Insert_OnClick(object sender, RoutedEventArgs e)
        {
            var p = await _personManager.Create(new Person(Firstname.Text, Lastname.Text, Phone.Text));

            ContactsList.Items.Add(p);
        }

        private async void Update_OnClick(object sender, RoutedEventArgs e)
        {
            var selected = ContactsList.SelectedItem as Person;

            if (selected != null)
            {
                var updatedPerson = await _personManager.Update(new Person(selected.Id, Firstname.Text, Lastname.Text, Phone.Text));
                var selectedIndex = ContactsList.SelectedIndex;
                ContactsList.Items[selectedIndex] = updatedPerson;
            }
        }

        private async void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            var selected = ContactsList.SelectedItem as Person;

            if (selected != null)
            {
                AddressList.Items.Clear();
                ContactsList.Items.Remove(selected);
                await _personManager.Delete(selected);

                Firstname.Clear();
                Lastname.Clear();
                Phone.Clear();
                Address.Clear();
            }
        }

        private async void ContactsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ContactsList.SelectedItem as Person;

            if (selected != null)
            {
                Firstname.Text = selected.Firstname;
                Lastname.Text = selected.Lastname;
                Phone.Text = selected.Phone;
                Address.Clear();

                AddressList.Items.Clear();
                var addresses = await _addressManager.Read(selected);
                addresses.ToList().ForEach(address => AddressList.Items.Add(address));
            }
        }

        private async void InsertAddress_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedPerson = ContactsList.SelectedItem as Person;

            if (selectedPerson != null)
            {
                var a = await _addressManager.Create(new Address(Address.Text, selectedPerson.Id));

                AddressList.Items.Add(a);
            }
        }

        private async void UpdateAddress_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedPerson = ContactsList.SelectedItem as Person;
            var selectedAddress = AddressList.SelectedItem as Address;

            if (selectedPerson != null && selectedAddress != null)
            {
                var updatedAddress = await _addressManager.Update(new Address(selectedAddress.Id, Address.Text, selectedPerson.Id));
                var selectedIndex = AddressList.SelectedIndex;
                AddressList.Items[selectedIndex] = updatedAddress;
            }
        }

        private async void DeleteAddress_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedAddress = AddressList.SelectedItem as Address;

            if (selectedAddress != null)
            {
                AddressList.Items.Remove(selectedAddress);
                await _addressManager.Delete(selectedAddress);
                Address.Clear();
            }
        }

        private void AddressList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = AddressList.SelectedItem as Address;

            if (selected != null)
            {
                Address.Text = selected.AddressString;
            }
        }
    }
}
