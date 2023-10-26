using PhoneBook.Interfaces;
using Newtonsoft.Json;
using PhoneBook.Models;
using System.Text;
using NuGet.Common;

namespace PhoneBook.Data
{
    public class ContactDataApi : IContactData
    {
        private HttpClient httpClient { get; set; }

        public ContactDataApi()
        {
            httpClient = new HttpClient();
        }

        public void DeleteContact(int id)
        {
            string url = @"https://localhost:7169/api/values/id?id=" + $"{id}";

            var r = httpClient.DeleteAsync(requestUri: url).Result;
        }

        /// <summary>
        /// Получает все контаты из базы даных, использую web API
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Contact> GetAllContact()
        {
            string url = @"https://localhost:7169/api/values";

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<IEnumerable<Contact>>(json);
        }

        /// <summary>
        /// Возвращает данные о контакте по его индентификатору, использую web API
        /// </summary>
        /// <param name="id">индентификатор контакта</param>
        /// <returns>Десерилозованный контакт</returns>
        public Contact GetContact(int? id) 
        {
            string url = @"https://localhost:7169/api/values/id?id=" + $"{id}";

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<Contact>(json);
        }

        public void UpdateContact(int id, Contact contact)
        {
            string url = @"https://localhost:7169/api/values/id?id=" + $"{id}";

            var r = httpClient.PutAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }

        /// <summary>
        /// Добавляет контакт в базу данных, используя web API
        /// </summary>
        /// <param name="newContact"></param>
        public void CreateContact(Contact newContact)
        {
            string url = @"https://localhost:7169/api/values";

            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(newContact), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }
    }
}
