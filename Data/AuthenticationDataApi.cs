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
            //BaseAddress = new Uri("https://a22219-3b93.g.d-f.pw/")
            BaseAddress = new Uri("https://localhost:7169/")
        };
        
        public async Task<string> Login(RequestLogin request)
        {
            string serelizeContact = JsonConvert.SerializeObject(request);

            var response = await httpClient.PostAsync(
               requestUri: "authentication/login",
               content: new StringContent(serelizeContact, Encoding.UTF8,
               mediaType: "application/json"));

            var d = response.EnsureSuccessStatusCode();

            var token = response.Content.ReadAsStringAsync();

            return token.Result;

        }
    }
}
