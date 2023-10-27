using PhoneBook.Models;

namespace PhoneBook.Interfaces
{
    public interface IContactData
    {
        IEnumerable<Contact> GetAllContact();
        Contact GetContact(int? id);
        void CreateContact(Contact newContact);
        void UpdateContact(int id, Contact contact);
        void DeleteContact(int id);
    }
}
