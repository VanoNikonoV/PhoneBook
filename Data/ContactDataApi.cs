using PhoneBook.Interfaces;
using Newtonsoft.Json;
using PhoneBook.Models;
using System.Text;
using System.Diagnostics;
using Azure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PhoneBook.Data
{
    //https://v2.d-f.pw/app/create-application

    public class ContactDataApi : IContactData
    {
        private static readonly HttpClient httpClient = new() 
        {
            BaseAddress = new Uri("https://a22273-3287.b.d-f.pw/"),
        };

        public ContactDataApi() {  }

        public async void DeleteContact(int id)
        {
            string url = $"values/id?id={id}";

            using HttpResponseMessage response = await httpClient.DeleteAsync(url);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"{jsonResponse}\n");
        }

        /// <summary>
        /// Получает все контаты из базы даных, использую web API
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IContact> GetAllContact()
        {
            string url = $"values";

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<IEnumerable<Contact>>(json);
        }

        /// <summary>
        /// Возвращает данные о контакте по его индентификатору, использую web API
        /// </summary>
        /// <param name="id">индентификатор контакта</param>
        /// <returns>Десерилозованный контакт</returns>
        public async Task<IContact> GetContact(int? id) 
        {
            try
            {
                AddTokenFofHeaders();

                string url = $"values/id?id=" + $"{id}";

                using HttpResponseMessage response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonContact = await response.Content.ReadAsStringAsync();

                    IContact contact = JsonConvert.DeserializeObject<Contact>(jsonContact);

                    return contact;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Error? error = await response.Content.ReadFromJsonAsync<Error>();

                    return NullContact.Create();
                }
                return NullContact.Create();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return NullContact.Create();
            }
        }

        public async void UpdateContact(int id, IContact contact)
        {
            string url = $"values/id?id=" + $"{id}";

            string serelizeContact = JsonConvert.SerializeObject(contact);

            try
            {
               var response = await httpClient.PutAsync(
               requestUri: url,
               content: new StringContent(serelizeContact, Encoding.UTF8,
               mediaType: "application/json"));

               Debug.Write($"\t\t\t" + response.StatusCode);
            }

            catch (HttpRequestException http) { Debug.WriteLine(http.Message); }

            catch (Exception ex) { Debug.WriteLine(ex.Message); }
           
        }

        /// <summary>
        /// Добавляет контакт в базу данных, используя web API
        /// </summary>
        /// <param name="newContact"></param>
        public async void CreateContact(IContact newContact)
        {
            string url = $"values";

            string serelizeContact = JsonConvert.SerializeObject(newContact);

            try
            {
                var response = await httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(serelizeContact, Encoding.UTF8,
                mediaType: "application/json"));

                Debug.Write($"\t\t\t" + response.EnsureSuccessStatusCode());
            }
            
            catch (HttpRequestException http) { Debug.WriteLine(http.Message); }

            catch (Exception http) { Debug.WriteLine(http.Message); }

        }

        private void AddTokenFofHeaders() 
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "bearer " + AccessForToken.Token);

            //BOOL
            //if (AccessForToken.Token != string.Empty)
            //{
            //    httpClient.DefaultRequestHeaders.Accept.Clear();
            //    httpClient.DefaultRequestHeaders.Add("Authorization", "bearer " + AccessForToken.Token);
            //    return true;
            //}
            //else { return false; }
        }

    }
}
