using Newtonsoft.Json;
using PhoneBook.Interfaces;
using PhoneBook.Models;
using System.Net.Http;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PhoneBook.Data
{
    
    public class AuthenticationDataApi :IAuthenticationData
    {
        public AuthenticationDataApi() { }

        private static readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://a22273-3287.b.d-f.pw/") 
            //BaseAddress = new Uri("https://localhost:7169/")
        };
        
        public async Task<string> Login(RequestLogin request)
        {
            string serelizeContact = JsonConvert.SerializeObject(request);

            var response = await httpClient.PostAsync(
               requestUri: "authentication/login",
               content: new StringContent(serelizeContact, Encoding.UTF8,
               mediaType: "application/json"));

            try
            {
                response.EnsureSuccessStatusCode();

                AccessForToken.Token = response.Content.ReadAsStringAsync().Result;

                return "Вы успехно вошли!";

            }
            catch (HttpRequestException httpEx)
            {
               return httpEx.Message;
            }

            //if (response.StatusCode == System.Net.HttpStatusCode.NotFound) 
            //{
            //    Error? error = await response.Content.ReadFromJsonAsync<Error>();

            //    return error.ToString();
            //}
            //else if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    var token = response.Content.ReadAsStringAsync();

            //    return token.Result;
            //}

            //return "неполучилось";
            //var d = response.EnsureSuccessStatusCode();
        }
    }
}
