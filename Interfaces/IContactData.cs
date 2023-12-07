using PhoneBook.Models;
using System.Net;

namespace PhoneBook.Interfaces
{
    public interface IContactData
    {
        Task<IEnumerable<IContact>> GetAllContact();
        Task<(IContact, HttpStatusCode)> GetContact(int? id);
        Task<HttpStatusCode> CreateContact(IContact newContact);
        Task<(IContact, HttpStatusCode)> UpdateContact(int id, IContact contact);
        Task<HttpStatusCode> DeleteContact(int id);
    }
}
