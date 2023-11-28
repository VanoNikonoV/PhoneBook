using PhoneBook.Models;

namespace PhoneBook.Interfaces
{
    public interface IContactData
    {
        IEnumerable<IContact> GetAllContact();
        Task<IContact> GetContact(int? id);
        void CreateContact(IContact newContact);
        void UpdateContact(int id, IContact contact);
        void DeleteContact(int id);
    }
}
