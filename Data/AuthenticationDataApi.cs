using Newtonsoft.Json;
using PhoneBook.Interfaces;
using PhoneBook.Models;
using System.Net.Http;
using System.Text;

namespace PhoneBook.Data
{
    
    public class AuthenticationDataApi :IAuthenticationData
    {
        public AuthenticationDataApi() { }

        private static readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://a21773-e6ea.c.d-f.pw/")
        };
        
        public async Task<string> Login(User user)
        {
            string url = @"https://a21773-e6ea.c.d-f.pw/authentication/login";

            string serelizeContact = JsonConvert.SerializeObject(user);

            var response = await httpClient.PostAsync(
               requestUri: url,
               content: new StringContent(serelizeContact, Encoding.UTF8,
               mediaType: "application/json"));

            return "token";

        }
    }
}
