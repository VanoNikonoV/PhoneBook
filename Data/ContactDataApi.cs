using PhoneBook.Interfaces;
using Newtonsoft.Json;
using PhoneBook.Models;
using System.Text;
using System.Diagnostics;
using Azure;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;

namespace PhoneBook.Data
{
    //https://v2.d-f.pw/app/create-application

    public class ContactDataApi : IContactData
    {
        private static readonly HttpClient httpClient = new() 
        {
            BaseAddress = new Uri("https://a22508-0df2.k.d-f.pw/"),
        };

        public ContactDataApi() {  }

        public async Task<HttpStatusCode> DeleteContact(int id)
        {
            if (!AddTokenFofHeaders()) return HttpStatusCode.Unauthorized;
            try
            {
                string url = $"values/id?id={id}";

                using HttpResponseMessage response = await httpClient.DeleteAsync(url);

                return response.StatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return  HttpStatusCode.NotFound;
            }

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
        public async Task<(IContact, HttpStatusCode)> GetContact(int? id) 
        {
            if (!AddTokenFofHeaders()) return (NullContact.Create(), HttpStatusCode.Unauthorized);
            
            try
            {
                //AddTokenFofHeaders();

                string url = $"values/id?id=" + $"{id}";

                using HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContact = await response.Content.ReadAsStringAsync();

                    IContact contact = JsonConvert.DeserializeObject<Contact>(jsonContact);

                    return (contact, response.StatusCode);
                }
                else { return (NullContact.Create(), response.StatusCode);}

                //response.EnsureSuccessStatusCode();

                //if (response.StatusCode == HttpStatusCode.OK)
                //{
                //    string jsonContact = await response.Content.ReadAsStringAsync();

                //    IContact contact = JsonConvert.DeserializeObject<Contact>(jsonContact);

                //    return (contact, response.StatusCode);
                //}
                //if (response.StatusCode == HttpStatusCode.Unauthorized)
                //{
                //    Error? error = await response.Content.ReadFromJsonAsync<Error>();

                //    return (NullContact.Create(), response.StatusCode);
                //}
                //if (response.StatusCode == HttpStatusCode.Forbidden)
                //{
                //    Error? error = await response.Content.ReadFromJsonAsync<Error>();

                //    return (NullContact.Create(), response.StatusCode);
                //}

                //return (NullContact.Create(), response.StatusCode);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return (NullContact.Create(), HttpStatusCode.NotFound);
            }
        }

        public async Task<(IContact, HttpStatusCode)> UpdateContact(int id, IContact contact)
        {
            if (!AddTokenFofHeaders()) return (NullContact.Create(), HttpStatusCode.Unauthorized);

            string url = $"values/id?id=" + $"{id}";

            try
            {
               string serelizeContact = JsonConvert.SerializeObject(contact);
               var response = await httpClient.PutAsync(
               requestUri: url,
               content: new StringContent(serelizeContact, Encoding.UTF8,
               mediaType: "application/json"));

               if (response.IsSuccessStatusCode) 
               {
                    string jsonContact = await response.Content.ReadAsStringAsync();

                    IContact returnContact = JsonConvert.DeserializeObject<IContact>(jsonContact);

                    return (returnContact, response.StatusCode);
               }
               else { return (NullContact.Create(), response.StatusCode); }

            }

            catch (HttpRequestException http) 
            {
                Debug.WriteLine(http.Message);
                return (NullContact.Create(), HttpStatusCode.NotFound);
            }

            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return (NullContact.Create(), HttpStatusCode.NotFound);
            }
           
        }

        /// <summary>
        /// Добавляет контакт в базу данных, используя web API
        /// </summary>
        /// <param name="newContact"></param>
        public async Task<(IContact, HttpStatusCode)> CreateContact(IContact newContact)
        {
            if(!AddTokenFofHeaders()) return (NullContact.Create(), HttpStatusCode.Unauthorized);

            string url = $"values";

            try
            {
                string serelizeContact = JsonConvert.SerializeObject(newContact);
                var response = await httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(serelizeContact, Encoding.UTF8,
                mediaType: "application/json"));

                if (response.IsSuccessStatusCode) 
                {
                    string jsonContact = await response.Content.ReadAsStringAsync();

                    IContact returnContact = JsonConvert.DeserializeObject<IContact>(jsonContact);

                    return (returnContact, response.StatusCode);
                }
                else { return (NullContact.Create(), response.StatusCode); }
            }

            catch (HttpRequestException http)
            {
                Debug.WriteLine(http.Message);
                return (NullContact.Create(), HttpStatusCode.NotFound);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return (NullContact.Create(), HttpStatusCode.NotFound);
            }

        }

        /// <summary>
        /// Добавляет jwt-токен в заголовок ответа 
        /// </summary>
        /// <returns>true - при успешной регистрации </returns>
        private bool AddTokenFofHeaders() 
        {
            //httpClient.DefaultRequestHeaders.Accept.Clear();
            //httpClient.DefaultRequestHeaders.Add("Authorization", "bearer " + AccessForToken.Token);

            //BOOL
            if (AccessForToken.Token != string.Empty)
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "bearer " + AccessForToken.Token);
                return true;
            }
            else { return false; }
        }

    }
}
