﻿using PhoneBook.Interfaces;
using Newtonsoft.Json;
using PhoneBook.Models;
using System.Text;
using System.Diagnostics;

namespace PhoneBook.Data
{
    //https://v2.d-f.pw/app/create-application

    public class ContactDataApi : IContactData
    {
        private static readonly HttpClient httpClient = new() 
        { 
            //BaseAddress = new Uri("https://a22219-3b93.g.d-f.pw/"),
            BaseAddress = new Uri("https://localhost:7169/"),
        };

        public ContactDataApi() 
        {
            //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", accessToken);
        }

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
        public IEnumerable<Contact> GetAllContact()
        {
            string url = $"values/";

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<IEnumerable<Contact>>(json);
        }

        /// <summary>
        /// Возвращает данные о контакте по его индентификатору, использую web API
        /// </summary>
        /// <param name="id">индентификатор контакта</param>
        /// <returns>Десерилозованный контакт</returns>
        public async Task<Contact> GetContact(int? id) 
        {
            string url = $"values/id?id=" + $"{id}";

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessForToken.Token);

            string json = await httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<Contact>(json);
        }

        public async void UpdateContact(int id, Contact contact)
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
        public async void CreateContact(Contact newContact)
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

        


    }
}
